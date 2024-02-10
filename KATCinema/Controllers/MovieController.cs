using KATCinema.Models;
using KATCinema.Utils.DBConnection;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;

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
            movie.Sessions.RemoveAll(session => session.Id == id);
            for(int i = 0;i < movie.Sessions.Count;i++)
            { 
                if (movie.Sessions[i].StartTime.Date < DateTime.Now.Date)
                {
                    movie.Sessions.Remove(movie.Sessions[i]);
                    i--;
                }
            }
            return View(movie);
        }
    }
}

