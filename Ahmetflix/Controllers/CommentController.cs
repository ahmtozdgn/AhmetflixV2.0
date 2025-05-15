using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Ahmetflix.Data;
using Ahmetflix.Models;
using System.Linq;
using System.Threading.Tasks;

namespace Ahmetflix.Controllers
{
    [Authorize]
    public class CommentController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<AppUser> _userManager;

        public CommentController(ApplicationDbContext context, UserManager<AppUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        [HttpPost]
        public async Task<IActionResult> AddMovieComment(int movieId, string content)
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null) return NotFound();

            var movie = await _context.Movies.FindAsync(movieId);
            if (movie == null) return NotFound();

            var comment = new Comment
            {
                Content = content,
                AppUserId = user.Id,
                MovieId = movieId,
                CreatedAt = System.DateTime.UtcNow
            };

            _context.Comments.Add(comment);
            await _context.SaveChangesAsync();

            return RedirectToAction("Details", "Movie", new { id = movieId });
        }

        [HttpPost]
        public async Task<IActionResult> AddSeriesComment(int seriesId, string content)
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null) return NotFound();

            var series = await _context.Series.FindAsync(seriesId);
            if (series == null) return NotFound();

            var comment = new Comment
            {
                Content = content,
                AppUserId = user.Id,
                SeriesId = seriesId,
                CreatedAt = System.DateTime.UtcNow
            };

            _context.Comments.Add(comment);
            await _context.SaveChangesAsync();

            return RedirectToAction("Details", "Series", new { id = seriesId });
        }

        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null) return NotFound();

            var comment = await _context.Comments.FindAsync(id);
            if (comment == null) return NotFound();

            if (comment.AppUserId != user.Id && !User.IsInRole("Admin"))
            {
                return Forbid();
            }

            _context.Comments.Remove(comment);
            await _context.SaveChangesAsync();

            if (comment.MovieId.HasValue)
            {
                return RedirectToAction("Details", "Movie", new { id = comment.MovieId });
            }
            else if (comment.SeriesId.HasValue)
            {
                return RedirectToAction("Details", "Series", new { id = comment.SeriesId });
            }

            return RedirectToAction("Index", "Home");
        }
    }
} 