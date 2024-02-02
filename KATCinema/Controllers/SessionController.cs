using KATCinema.Models;
using KATCinema.Utils.DBConnection;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;

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
        public IActionResult Booking(int id)
        {
            Session sesion = _context.Sessions.Include(session => session.Hall).FirstOrDefault(x => x.Id == id);
            return View(sesion);
        }
    }
}

