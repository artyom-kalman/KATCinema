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
            Session session = _context.Sessions.
                Include(session => session.Movie).
                Include(session => session.Hall).
                Include(session => session.Hall.Rows).
                ThenInclude(row => row.Seats).
                Include(session => session.Reservations).
                ThenInclude(reservation => reservation.ReservedSeats).FirstOrDefault(x => x.Id == id);

            List<ReservedSeat> newReservedSeats = new List<ReservedSeat>();

            foreach (Row row in session.Hall.Rows)
            {
                foreach(Seat seat in row.Seats)
                {
                    if (Request.Form[$"{seat.Id}"] == "true")
                    {
                        if(_context.ReservedSeats.Where(reservedSeat => reservedSeat.Reservation.SessionId == session.Id && reservedSeat.Seat.Id == seat.Id).Count() == 0)
                            newReservedSeats.Add(new ReservedSeat{
                                SeatId = seat.Id,
                            });
                        else
                        {
                            TempData["Error"] = "Выбранное место уже забронировано";
                            return View(session);
                        }
                    }
                }
            }
            if (newReservedSeats.Count > 0)
            {
                Reservation reservation = new Reservation
                {
                    UserId = _context.Users.FirstOrDefault(user => user.UserName == User.Identity.Name).Id,
                    SessionId = id
                };
                _context.Reservations.AddAsync(reservation);
                _context.SaveChanges();

                foreach (ReservedSeat reservedSeat in newReservedSeats)
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

