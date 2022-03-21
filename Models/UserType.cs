using System.ComponentModel.DataAnnotations;

namespace Ticket_Sell.Models
{
    public class UserType
    {
        [Key]
        public int  Id { get; set; }
        public string Type { get; set; }
    }
}
