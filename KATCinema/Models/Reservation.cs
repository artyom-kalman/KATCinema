using System.ComponentModel.DataAnnotations;

namespace KATCinema.Models
{
    public class Reservation
    {
        [Key]
        public int Id { get; set; }
        public int UserId { get; set; }
        public int ReservedSeatId { get; set; }
    }
}
