using KATCinema.Models;
using KATCinema.Utils.DBConnection;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace KATCinema.Controllers
{
    public class MovieController : Controller
    {
        private readonly ApplicationDbContext _context;

        public MovieController(ApplicationDbContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            List<Movie> movies = _context.Movies.ToList();
            return View(movies);
        }
        public IActionResult Detail(int id)
        {
            Movie movie = _context.Movies.Include(movie => movie.Sessions).FirstOrDefault(x => x.Id == id);
            //List<Session> sesions = _context.Sessions.Where(x => x.MovieId == id).ToList();
            return View(movie);
        }
    }
}

