using System.ComponentModel.DataAnnotations.Schema;

namespace Ticket_Sell.Models
{
    public class UserTicketHistory
    {
        public int Id { get; set; }
        [ForeignKey("UserId")]
        public int UserId { get; set; }
        public virtual User User { get; set; }
        [ForeignKey("TicketId")]
        public int TicketId { get; set; }
        public virtual Ticket Ticket { get; set; }
        public int NumberOfTickets { get; set; }
        public DateTime PurchaseTime { get; set; }

    }
}
