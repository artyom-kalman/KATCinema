using KATCinema.Models;

namespace KATCinema.ViewModels
{
    public class MovieViewModel
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public int Duration { get; set; }
        public IFormFile Poster { get; set; }
    }
}
