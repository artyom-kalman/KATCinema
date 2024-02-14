using KATCinema.Models;
using System.ComponentModel.DataAnnotations;

namespace KATCinema.ViewModels
{
    public class MovieViewModel
    {
        [Display(Name = "Название")]
        public string Title { get; set; }
        [Display(Name = "Описание")]
        public string Description { get; set; }
        [Display(Name = "Длительность")]
        public int Duration { get; set; }
        [Display(Name = "Постер")]
        public IFormFile Poster { get; set; }
    }
}
