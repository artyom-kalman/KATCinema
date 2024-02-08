using System.ComponentModel.DataAnnotations.Schema;

namespace KATCinema.Models
{
    public class Seat
    {
        public int Id { get; set; }
        [ForeignKey("Row")]
        public int RowId { get; set; }
        public Row Row { get; set; }

    }
}
