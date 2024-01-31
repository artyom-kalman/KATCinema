using System.ComponentModel.DataAnnotations;

namespace KATCinema.Models
{
    public class ReservedSeat
    {
        [Key]
        public int Id { get; set; }
        public int ReservationId { get; set; }
        public int RowNumber { get; set; }
        public int SeatNumber { get; set; }
    }
}
