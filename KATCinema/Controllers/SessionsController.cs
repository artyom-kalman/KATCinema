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

namespace KATCinema.Controllers
{
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
        public async Task<IActionResult> Create([Bind("Id,MovieId,StartTime,HallId,TicketPrice")] SessionViewModel sessionViewModel)
        {
            if (ModelState.IsValid)
            {
                //var hallId = _context.Halls.FirstOrDefault(h => h.Name == sessionViewModel.HallName).Id;
                //var movieId= _context.Movies.FirstOrDefault(m => m.Title == sessionViewModel.MovieTitle).Id;
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

        // GET: Sessions/Edit/5
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
            ViewData["HallId"] = new SelectList(_context.Halls, "Id", "Id", session.HallId);
            ViewData["MovieId"] = new SelectList(_context.Movies, "Id", "Id", session.MovieId);
            return View(session);
        }

        // POST: Sessions/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,MovieId,StartTime,HallId,TicketPrice")] Session session)
        {
            if (id != session.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
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
            ViewData["HallId"] = new SelectList(_context.Halls, "Id", "Id", session.HallId);
            ViewData["MovieId"] = new SelectList(_context.Movies, "Id", "Id", session.MovieId);
            return View(session);
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
