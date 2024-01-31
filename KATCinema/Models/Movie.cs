using System.ComponentModel.DataAnnotations;

namespace KATCinema.Models
{
    public class Movie
    {
        [Key]
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public int Duration { get; set; }
        public string Poster { get; set; }
    }
}
