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
                Include(session => session.Hall.Rows).
                ThenInclude(row => row.Seats).
                Include(session => session.Reservations).
                ThenInclude(reservation => reservation.ReservedSeats).FirstOrDefault(x => x.Id == id);
            return View(session);
        }

        [HttpPost]
        public IActionResult Booking(int id, bool q)
        {
            Session sesion = _context.Sessions.
                Include(session => session.Hall).
                Include(session => session.Hall.Rows).
                ThenInclude(row => row.Seats).FirstOrDefault(x => x.Id == id);
            List<ReservedSeat> reservedSeats= new List<ReservedSeat>();
            foreach(Row row in sesion.Hall.Rows)
            {
                foreach(Seat seat in row.Seats)
                {
                    if (Request.Form[$"{seat.Id}"] == "true")
                    {
                        reservedSeats.Add(new ReservedSeat{
                            SeatId = seat.Id,
                        });
                    }
                }
            }
            if (reservedSeats.Count > 0)
            {
                Reservation reservation = new Reservation
                {
                    UserId = _context.Users.FirstOrDefault(user => user.UserName == User.Identity.Name).Id,
                    SessionId = id
                };
                _context.Reservations.AddAsync(reservation);
                _context.SaveChanges();
                foreach (ReservedSeat reservedSeat in reservedSeats)
                {
                    reservedSeat.ReservationId = reservation.Id;
                    _context.ReservedSeats.AddAsync(reservedSeat);
                }
                _context.SaveChanges();
            }
            return RedirectToAction("Index","Account");
        }
    }
}

