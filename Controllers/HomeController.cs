using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using System.Text.RegularExpressions;
using Ticket_Sell.Data;
using Ticket_Sell.Helpers;
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
        public async Task<ActionResult> Register(RegistrationModel model, int UserTypeId)
        {

            if (!ModelState.IsValid)
            {
                int alphabeticNumbers = Regex.Matches(model.Username, @"[a-zA-Z]").Count;
                if (alphabeticNumbers == 0)
                {
                    TempData["usernameError"] = "მომხმარებლის სახელი უნდა შეიცავდეს მინიმუმ 1 ასოს!";
                }
                if (model.Password != model.ConfirmPassword)
                {
                    TempData["passwordsMatchErr"] = "პაროლები არ ემთხვევა";
                }
                if (model.PN.Length != 11 || string.IsNullOrEmpty(model.PN))
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
                string PassHash = HashPassword.GetPassHash(model.Password);
                string Salt = HashPassword.saltstring;
                User toregister = new();
                toregister.Username = model.Username;
                toregister.Email = model.Email;
                toregister.Mobile = model.Mobile;
                toregister.PN = model.PN;
                toregister.UserTypeId = UserTypeId;
                toregister.Password = PassHash;
                toregister.Salt = Salt;
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
            var prs = await _db.Users.Where(x => (x.Email == Username) || (x.Mobile == Username)).FirstOrDefaultAsync();
            if (prs == null)
            {
                TempData["logerror"] = "სახელი ან პაროლი არასწორია";
                return View();
            }
            string Salt = prs.Salt;
            string PassHash = HashPassword.GetPassHash(password);
            
            if (prs.Password != PassHash)
            {
                TempData["logerror"] = "სახელი ან პაროლი არასწორია";
                return View();
            }else if (password == PassHash)
            {
                return Ok("Logged In");
            }
            return Ok("Invalid Login Attempt");
        }

        [HttpGet]
        public async Task<ActionResult> ForgotPassword()
        {
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> ForgotPassword(string email)
        {
            var per = await _db.Users.Where(x => x.Email == email).FirstOrDefaultAsync();
            if(per == null)
            {
                TempData["notFound"] = "ამ მეილით მომხმარებელი ვერ მოიძებნა";
                return View();
            }
            ViewData["personId"] = per.Id;
            return View("ResetPassword");
        }

        [HttpGet]
        public async Task<ActionResult> ResetPassword()
        {
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> ResetPassword(string newPass, string confPass, int persId)
        {
            if(newPass != confPass)
            {
                TempData["dontMatch"] = "Passwords does not match! ";
                return View();
            }
            var perToChange = await _db.Users.Where(k => k.Id == persId).SingleOrDefaultAsync();
            if(perToChange == null)
            {
                TempData["ChangeError"] = "could not find user";
                return View();
            }
            if(perToChange.Password == newPass)
            {
                TempData["SamePass"] = "new password can not be old password";
                return View();
            }
            perToChange.Password = newPass;
            _db.SaveChanges();
            TempData["ChangeSuccess"] = "password changed succesfully";
            return RedirectToAction("Login", "Home");
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