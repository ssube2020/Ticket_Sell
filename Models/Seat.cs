using System.ComponentModel.DataAnnotations.Schema;

namespace Ticket_Sell.Models
{
    public class Seat
    {

        public int Id { get; set; }
        public int Tier { get; set; }
        public int Section { get; set; }
        public int Row { get; set; }
        public int Place { get; set; }
        [ForeignKey("TicketId")]
        public int TicketId { get; set; }
        public virtual Ticket Ticket { get; set; }
        [ForeignKey("StadiumId")]
        public int StadiumId { get; set; }
        public virtual Stadium Stadium { get; set; }
        public string SeatName { get; set; }
        public char SeatSymbol { get; set; }

    }
}
