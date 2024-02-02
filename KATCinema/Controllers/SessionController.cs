using KATCinema.Models;
using KATCinema.Utils.DBConnection;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace KATCinema.Controllers
{
    public class SessionController : Controller
    {
        private readonly ApplicationDbContext _context;

        public SessionController(ApplicationDbContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            List<Session> sessions = _context.Sessions.Include(x => x.Movie).ToList();
            return View(sessions);
        }
        public IActionResult Detail(int moveId)
        {
            List<Session> sesions = _context.Sessions.Where(x => x.MovieId == moveId).ToList();
            return View(sesions);
        }
    }
}

