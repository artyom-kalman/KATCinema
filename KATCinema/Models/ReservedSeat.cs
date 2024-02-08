using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace KATCinema.Models
{
    public class ReservedSeat
    {
        [Key]
        public int Id { get; set; }
        [ForeignKey("Reservation")]
        public int ReservationId { get; set; }
        public Reservation Reservation { get; set; }
        public int RowNumber { get; set; }
        public int SeatNumber { get; set; }
        [ForeignKey("Seat")]
        public int SeatId { get; set; }
        public Seat Seat { get; set; }
    }
}
