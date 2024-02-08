using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace KATCinema.Models
{
    public class Seat
    {
        [Key]
        public int Id { get; set; }
        public int SeatNumber { get; set; }
        [ForeignKey("Row")]
        public int RowId { get; set; }
        public Row Row { get; set; }

    }
}
