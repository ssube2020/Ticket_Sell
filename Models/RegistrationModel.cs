using System.ComponentModel.DataAnnotations;

namespace Ticket_Sell.Models
{
    public class RegistrationModel
    {

        [Required]
        [StringLength(50, ErrorMessage = "The {0} must be 11 characters long.", MinimumLength = 3)]
        [Display(Name = "Username")]
        public string Username { get; set; }

        [Required]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [Required]
        [StringLength(11, ErrorMessage = "The {0} must be 11 characters long.", MinimumLength = 9)]
        public string PN { get; set; }

        [Required]
        [StringLength(9, ErrorMessage = "please enter this format: 555555555", MinimumLength = 9)]
        public string Mobile { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirm password")]
        [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }

    }
}
