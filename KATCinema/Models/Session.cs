using System.ComponentModel.DataAnnotations;

namespace KATCinema.Models
{
    public class Session
    {
        [Key]
        public int Id { get; set; }
        public int MovieId { get; set; }
        public DateTime StartTime { get; set; }
        public int HallId { get; set; }
        public decimal TicketPrice { get; set; }
    }
}
