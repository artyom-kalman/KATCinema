using KATCinema.Models;
using KATCinema.Utils;
using KATCinema.Utils.DBConnection;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace KATCinema.Controllers
{
   [CustomAuthorizationFilter]
    public class SessionController : Controller
    {
        private ApplicationDbContext _context;
        private IHttpContextAccessor _httpContextAccessor;

        public SessionController(ApplicationDbContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            List<Session> sessions = _context.Sessions.Include(session => session.Reservations).ToList();
            return View(sessions);
        }
        [HttpGet]
        public IActionResult Booking(int id)
        {
            Session session = _context.Sessions.
                Include(session => session.Movie).
                Include(session => session.Hall).
                Include(session => session.Reservations).
                ThenInclude(reservation => reservation.ReservedSeats).FirstOrDefault(x => x.Id == id);
            return View(session);
        }

        [HttpPost]
        public IActionResult Booking(int id, bool q)
        {
            Session sesion = _context.Sessions.Include(session => session.Hall).FirstOrDefault(x => x.Id == id);
            int seatInRow = sesion.Hall.TotalSeats / sesion.Hall.TotalRows;
            Reservation reservation = new Reservation
            {
                UserId = _context.Users.FirstOrDefault(user => user.UserName == User.Identity.Name).Id,
                SessionId = id
            };
            _context.Reservations.AddAsync(reservation);
            _context.SaveChanges();
            for (int i = 1;i<=sesion.Hall.TotalRows; i++)
            {
                for (int j = 1; j <= sesion.Hall.TotalRows; j++)
                {
                    string qe = Request.Form[$"{i} {j}"];
                    if (Request.Form[$"{i} {j}"] == "true")
                    {
                        ReservedSeat reservedSeat = new ReservedSeat
                        {
                            ReservationId = reservation.Id,
                            RowNumber = i,
                            SeatNumber = j
                        };
                        _context.ReservedSeats.AddAsync(reservedSeat);
                        _context.SaveChanges();
                    }
                }
            }
            return RedirectToAction("Index","Account");
        }
    }
}

