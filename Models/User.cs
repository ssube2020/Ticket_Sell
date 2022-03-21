using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Ticket_Sell.Models
{
    public class User
    {
        [Key]
        public int Id { get; set; }

        [Required]
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
        [ForeignKey("UserTypeId")]
        public int UserTypeId { get; set; }
        public virtual UserType UserType { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [Required]
        public string Salt { get; set; }

        [Required]
        public bool? Status { get; set; }
    }
}
