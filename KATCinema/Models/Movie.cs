using Microsoft.Extensions.Hosting;
using System.ComponentModel.DataAnnotations;

namespace KATCinema.Models
{
    public class Movie
    {
        [Key]
        public int Id { get; set; }
        [Display(Name = "Название")]
        public string Title { get; set; }
        [Display(Name = "Описание")]
        public string Description { get; set; }
        [Display(Name = "Длительность")]
        public int Duration { get; set; }
        public string PosterUrl { get; set; }
        public string PosterId { get; set; }
        public List<Session>? Sessions { get; set; }
    }
}
