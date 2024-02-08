using System.ComponentModel.DataAnnotations.Schema;

namespace KATCinema.Models
{
    public class Row
    {
        public int Id { get; set; }
        [ForeignKey("Hall")]
        public int HallId { get; set; }
        public Hall Hall { get; set; }
        public int RowNumber { get; set; }
        public List<Seat> Seats { get; set; }
    }
}
