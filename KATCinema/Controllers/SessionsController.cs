using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using KATCinema.Models;
using KATCinema.Utils.DBConnection;
using KATCinema.ViewModels;
using Microsoft.AspNetCore.Authorization;
using KATCinema.Utils;

namespace KATCinema.Controllers
{
    [CustomAuthorizationFilter(Role = "Admin")]
    public class SessionsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public SessionsController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var sessions = _context.Sessions
                .Include(s => s.Hall)
                .Include(s => s.Movie)
                .OrderBy(s => s.StartTime)
                .ToList();

            sessions.ForEach(s =>
            {
                s.StartTime = s.StartTime.AddHours(10);
            });

            for (int i = 0; i < sessions.Count; i++)
            {
                if (sessions[i].StartTime < DateTime.Now)
                {
                    sessions.RemoveAt(i);
                    i--;
                }
            }



            return View(sessions);
        }

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var session = await _context.Sessions
                .Include(s => s.Hall)
                .Include(s => s.Movie)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (session == null)
            {
                return NotFound();
            }

            return View(session);
        }

        [HttpGet]
        public IActionResult Create()
        {
            ViewData["HallName"] = new SelectList(_context.Halls, "Id", "Name");    
            ViewData["MovieTitle"] = new SelectList(_context.Movies, "Id", "Title");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(SessionViewModel sessionViewModel)
        {
            if (ModelState.IsValid)
            {
                Session newSession = new Session()
                {
                    HallId = sessionViewModel.HallId,
                    MovieId = sessionViewModel.MovieId,
                    StartTime = sessionViewModel.StartTime.ToUniversalTime(),
                    TicketPrice = sessionViewModel.TicketPrice
                };
                _context.Add(newSession);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["HallName"] = new SelectList(_context.Halls, "Id", "Name", sessionViewModel.HallId);
            ViewData["MovieId"] = new SelectList(_context.Movies, "Id", "Title", sessionViewModel.MovieId);
            return View(sessionViewModel);
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var session = await _context.Sessions.FindAsync(id);
            if (session == null)
            {
                return NotFound();
            }
            var sessionViewModel = new SessionViewModel()
            {
                HallId = session.HallId,
                Id = session.Id,
                MovieId = session.MovieId,
                StartTime = session.StartTime.AddHours(10),
                TicketPrice = session.TicketPrice
            };
            ViewData["HallName"] = new SelectList(_context.Halls, "Id", "Name", session.HallId);
            ViewData["MovieTitle"] = new SelectList(_context.Movies, "Id", "Title", session.MovieId);
            return View(sessionViewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, SessionViewModel sessionViewModel)
        {
            if (id != sessionViewModel.Id)
            {
                return NotFound();
            }

            if (!ModelState.IsValid)
            {
                ViewData["HallName"] = new SelectList(_context.Halls, "Id", "Name", sessionViewModel.HallId);
                ViewData["MovieTitle"] = new SelectList(_context.Movies, "Id", "Title", sessionViewModel.MovieId);
                return View(sessionViewModel);
            }

            var session = new Session()
            {
                HallId = sessionViewModel.HallId,
                Id = sessionViewModel.Id,
                MovieId = sessionViewModel.MovieId,
                StartTime = sessionViewModel.StartTime.ToUniversalTime(),
                TicketPrice = sessionViewModel.TicketPrice
            };

            try
            {
                _context.Update(session);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!SessionExists(session.Id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }
            return RedirectToAction(nameof(Index));
        }

        // GET: Sessions/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var session = await _context.Sessions
                .Include(s => s.Hall)
                .Include(s => s.Movie)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (session == null)
            {
                return NotFound();
            }

            return View(session);
        }

        // POST: Sessions/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var session = await _context.Sessions.FindAsync(id);
            if (session != null)
            {
                _context.Sessions.Remove(session);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool SessionExists(int id)
        {
            return _context.Sessions.Any(e => e.Id == id);
        }
    }
}
