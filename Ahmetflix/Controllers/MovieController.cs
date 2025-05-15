using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Ahmetflix.Data;
using Ahmetflix.Models;
using Microsoft.AspNetCore.Authorization;
using System.Linq;
using System.Threading.Tasks;

namespace Ahmetflix.Controllers
{
    public class MovieController : Controller
    {
        private readonly ApplicationDbContext _context;

        public MovieController(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index(string? search, int? categoryId)
        {
            var movies = _context.Movies.AsQueryable();

            if (!string.IsNullOrEmpty(search))
            {
                movies = movies.Where(m => m.Title != null && m.Title.Contains(search));
            }

            if (categoryId.HasValue)
            {
                movies = movies.Where(m => m.CategoryId == categoryId);
            }

            ViewBag.Categories = await _context.Categories.ToListAsync();
            return View(await movies.ToListAsync());
        }

        public async Task<IActionResult> Details(int id)
        {
            var movie = await _context.Movies
                .Include(m => m.Category)
                .FirstOrDefaultAsync(m => m.Id == id);

            if (movie == null)
            {
                return NotFound();
            }

            return View(movie);
        }

        [Authorize]
        public async Task<IActionResult> Watch(int id)
        {
            var movie = await _context.Movies.FindAsync(id);
            if (movie == null)
            {
                return NotFound();
            }

            return View(movie);
        }

        // GET: Movie/Create
        [Authorize(Roles = "Admin")]
        public IActionResult Create()
        {
            ViewBag.Categories = _context.Categories.ToList();
            return View();
        }

        // POST: Movie/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Create([Bind("Title,Description,ReleaseDate,Duration,Rating,ImageUrl,BackdropUrl,VideoUrl,IsNew,IsPopular")] Movie movie, int[] categoryIds)
        {
            if (ModelState.IsValid)
            {
                if (categoryIds != null)
                {
                    movie.Categories = await _context.Categories
                        .Where(c => categoryIds.Contains(c.Id))
                        .ToListAsync();
                }

                _context.Add(movie);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            ViewBag.Categories = _context.Categories.ToList();
            return View(movie);
        }

        // GET: Movie/Edit/5
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var movie = await _context.Movies
                .Include(m => m.Categories)
                .FirstOrDefaultAsync(m => m.Id == id);

            if (movie == null)
            {
                return NotFound();
            }

            ViewBag.Categories = _context.Categories.ToList();
            return View(movie);
        }

        // POST: Movie/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Title,Description,ReleaseDate,Duration,Rating,ImageUrl,BackdropUrl,VideoUrl,IsNew,IsPopular")] Movie movie, int[] categoryIds)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var existingMovie = await _context.Movies
                        .Include(m => m.Categories)
                        .FirstOrDefaultAsync(m => m.Id == id);

                    if (existingMovie == null)
                    {
                        return NotFound();
                    }

                    // Update basic properties
                    existingMovie.Title = movie.Title;
                    existingMovie.Description = movie.Description;
                    existingMovie.ReleaseDate = movie.ReleaseDate;
                    existingMovie.Duration = movie.Duration;
                    existingMovie.Rating = movie.Rating;
                    existingMovie.ImageUrl = movie.ImageUrl;
                    existingMovie.BackdropUrl = movie.BackdropUrl;
                    existingMovie.VideoUrl = movie.VideoUrl;
                    existingMovie.IsNew = movie.IsNew;
                    existingMovie.IsPopular = movie.IsPopular;

                    // Update categories
                    if (existingMovie.Categories != null)
                    {
                        existingMovie.Categories.Clear();
                        if (categoryIds != null)
                        {
                            var categories = await _context.Categories
                                .Where(c => categoryIds.Contains(c.Id))
                                .ToListAsync();
                            existingMovie.Categories = categories;
                        }
                    }
                    else
                    {
                        if (categoryIds != null)
                        {
                            var categories = await _context.Categories
                                .Where(c => categoryIds.Contains(c.Id))
                                .ToListAsync();
                            existingMovie.Categories = categories;
                        }
                    }

                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
                catch
                {
                    // Log the exception or handle it as needed
                    return StatusCode(500, "An error occurred while updating the movie.");
                }
            }

            ViewBag.Categories = _context.Categories.ToList();
            return View(movie);
        }

        // GET: Movie/Delete/5
        [Authorize(Roles = "Admin")]
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

        // POST: Movie/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var movie = await _context.Movies.FindAsync(id);
            if (movie != null)
            {
                _context.Movies.Remove(movie);
                await _context.SaveChangesAsync();
            }
            return RedirectToAction(nameof(Index));
        }

        private bool MovieExists(int id)
        {
            return _context.Movies.Any(e => e.Id == id);
        }
    }
}


