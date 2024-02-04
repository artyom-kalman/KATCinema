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
            List<Session> sessions = _context.Sessions.Include(session => session.Movie).ToList();
            return View(sessions);
        }
        [HttpGet]
        public IActionResult Booking(int id)
        {
            Session sesion = _context.Sessions.Include(session => session.Hall).FirstOrDefault(x => x.Id == id);
            return View(sesion);
        }
        [HttpPost]
        public IActionResult Booking(int id, bool[] seat)
        {
            string seatChecked = "";
            Session sesion = _context.Sessions.Include(session => session.Hall).FirstOrDefault(x => x.Id == id);
            int seatInRow = sesion.Hall.TotalSeats / sesion.Hall.TotalRows;
            for (int i = 1;i<=sesion.Hall.TotalRows; i++)
            {
                for (int j = 1; j <= sesion.Hall.TotalRows; j++)
                {
                    string qe = Request.Form[$"{i} {j}"];
                    if (Request.Form[$"{i} {j}"] == "true")
                    {
                        seatChecked += $"Ряд:{i} | Место {j}\n";
                    }
                }
            }
            return Content(seatChecked);
        }
    }
}

