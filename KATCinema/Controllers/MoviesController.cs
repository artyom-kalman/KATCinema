﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using KATCinema.Models;
using KATCinema.Utils.DBConnection;
using KATCinema.ViewModels;
using KATCinema.Interfaces;
using KATCinema.Utils;

namespace KATCinema.Controllers
{
    [CustomAuthorizationFilter(Role = "Admin")]
    public class MoviesController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IPhotoService _photoService;

        public MoviesController(ApplicationDbContext context, IPhotoService photoService)
        {
            _context = context;
            _photoService = photoService;
        }

        // GET: Movies
        public async Task<IActionResult> Index()
        {
            return View(await _context.Movies.ToListAsync());
        }

        // GET: Movies/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var movie = await _context.Movies
                .FirstOrDefaultAsync(m => m.Id == id);
            if (movie == null)
            {
                return NotFound();
            }

            return View(movie);
        }

        [HttpGet]
        public IActionResult Create()
        {
            MovieViewModel movieViewModel = new MovieViewModel();
            return View(movieViewModel);
        }

        [HttpPost]
        public async Task<IActionResult> Create(MovieViewModel movieViewModel)
        {
            if (!ModelState.IsValid)
            {
                return View(movieViewModel);
            }

            var posterUploadResult = await _photoService.UploadPhotoAsync(movieViewModel.Poster);
            var posterUrl = posterUploadResult.url;
            var posterId = posterUploadResult.fileId;

            var newMovie = new Movie()
            {
                Title = movieViewModel.Title,
                Description = movieViewModel.Description,
                Duration = movieViewModel.Duration,
                PosterUrl = posterUrl,
                PosterId = posterId
            };

            _context.Movies.Add(newMovie);

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        // GET: Movies/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var movie = await _context.Movies.FindAsync(id);
            if (movie == null)
            {
                return NotFound();
            }
            return View(movie);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id","Title","Description","Duration","PosterUrl","PosterId")]Movie movie)
        {
            if (id != movie.Id)
            {
                return NotFound();
            }

            if (!ModelState.IsValid)
            {
                return View(movie);
            }

            try
            {
                _context.Update(movie);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!MovieExists(movie.Id))
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

        // GET: Movies/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var movie = await _context.Movies
                .FirstOrDefaultAsync(m => m.Id == id);
            if (movie == null)
            {
                return NotFound();
            }

            return View(movie);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var movie = await _context.Movies.FindAsync(id);
            if (movie != null)
            {
                _context.Movies.Remove(movie);
            }
            else
            {
                return RedirectToAction(nameof(Index));
            }

            var deletePosterResult = await _photoService.DeletePhotoAsync(movie.PosterId);
            if (!deletePosterResult)
            {
                return View();
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool MovieExists(int id)
        {
            return _context.Movies.Any(e => e.Id == id);
        }
    }
}
