using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Ticket_Sell.Models
{
    public class Ticket
    {
        [Key]
        public int Id { get; set; }
        public float Price { get; set; }
        [ForeignKey("MatchId")]
        public int MatchId { get; set; }
        public virtual Match Match { get; set; }

    }
}
