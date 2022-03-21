using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Ticket_Sell.Models
{
    public class SoldTicket
    {

        public int Id { get; set; }
        [ForeignKey("UserId")]
        public int UserId { get; set; }
        public virtual User User { get; set; }  
        [ForeignKey("SeatId")]
        public int SeatId { get; set; }
        public virtual Seat Seat { get; set; }
        [ForeignKey("TicketId")]
        public int TicketId { get; set; }
        public virtual Ticket Ticket { get; set; }
        [Required]
        public DateTime SoldDate { get; set; }

    }
}
