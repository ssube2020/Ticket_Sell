using System.ComponentModel.DataAnnotations.Schema;

namespace Ticket_Sell.Models
{
    public class Match
    {

        public int Id { get; set; }
        [ForeignKey("StadiumId")]
        public int StadiumId { get; set; }
        public virtual Stadium Stadium { get; set; }
        public string Team1 { get; set; }
        public string Team2 { get; set; }
        public DateTime StartDate { get; set; }
        public double Coefficient { get; set; }

    }
}
