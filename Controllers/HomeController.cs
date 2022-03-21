using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using System.Text.RegularExpressions;
using Ticket_Sell.Data;
using Ticket_Sell.Models;

namespace Ticket_Sell.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ApplicationDbContext _db;

        public HomeController(ILogger<HomeController> logger, ApplicationDbContext db)
        {
            _logger = logger;
            _db = db;   
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> Register(RegistrationModel model)
        {

            if (!ModelState.IsValid)
            {
                int alphabeticNumbers = Regex.Matches(model.Username, @"[a-zA-Z]").Count;
                if(alphabeticNumbers == 0)
                {
                    TempData["usernameError"] = "მომხმარებლის სახელი უნდა შეიცავდეს მინიმუმ 1 ასოს!";
                }
                if (model.Password != model.ConfirmPassword)
                {
                    TempData["passwordsMatchErr"] = "პაროლები არ ემთხვევა";
                }
                if (model.PN.Length != 11)
                {
                    TempData["PNerror"] = "შეიყვანეთ პირადი ნომერი სწორად! ";
                }
                if (model.Mobile.Length != 9)
                {
                    TempData["MobileError"] = "ტელეფონი ნომერი შეიყვანეთ შემდეგ ფორმატში - 555 55 55 55";
                }

                return View(ModelState);
            }
            var PerCheck = await _db.Users.Where(k => (k.Email == model.Email) || (k.PN == model.PN)).FirstOrDefaultAsync();
            if(PerCheck != null)
            {
                TempData["personexists"] = "მოცემული მეილით ან პირადი ნომრით მომხმარებელი უკვე არსებობს!";
                return View();
            } else
            {
                User toregister = new();
                toregister.Username = model.Username;
                toregister.Email = model.Email;
                toregister.Mobile = model.Mobile;
                toregister.PN = model.PN;
                toregister.UserTypeId = 1;
                toregister.Password = model.Password;
                toregister.Salt = "Salts1231!";
                toregister.Status = true;
                _db.Users.Add(toregister);
                _db.SaveChanges();
                TempData["regSuccess"] = "Registered Succesfully";
            }
            return View();
        }

        [HttpGet]
        public async Task<ActionResult> Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> Login(string Username, string password)
        {

            //if (!ModelState.IsValid)
            //{
            //    TempData["RequestError"] = ModelState.Values.SelectMany(e => e.Errors.Select(er => er.ErrorMessage));
            //    return View(ModelState);
            //}
            var prs = await _db.Users.Where(x => (x.Email == Username) || (x.Mobile == Username)).FirstOrDefaultAsync();
            if (prs == null)
            {
                TempData["logerror"] = "username or password is not correct";
                return View();
            }
            if(prs.Password == password)
            {
                return Ok("Logged In Succesfully");
            }
            //if (PerCheck == null)
            //{
            //    User toregister = new User();
            //    toregister.Username = model.Username;
            //    toregister.Email = model.Email;
            //    toregister.Mobile = model.Mobile;
            //    toregister.PN = model.PN;
            //    toregister.UserTypeId = 1;
            //    toregister.Password = model.Password;
            //    toregister.Salt = "Salts1231!";
            //    toregister.Status = true;
            //    _db.Users.Add(toregister);
            //    _db.SaveChanges();
            //}
            TempData["logerror1"] = "username or password is not correct";
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}