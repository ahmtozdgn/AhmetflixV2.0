using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Ahmetflix.Models;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using Ahmetflix.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace Ahmetflix.Controllers
{
    [Authorize(Roles = "Admin")]
    public class AdminController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IWebHostEnvironment _env;
        private readonly UserManager<AppUser> _userManager;

        public AdminController(ApplicationDbContext context, IWebHostEnvironment env, UserManager<AppUser> userManager)
        {
            _context = context;
            _env = env;
            _userManager = userManager;
        }

        public async Task<IActionResult> Index()
        {
            var viewModel = new AdminViewModel
            {
                Movies = await _context.Movies.ToListAsync(),
                Series = await _context.Series.ToListAsync(),
                Categories = await _context.Categories.ToListAsync(),
                Users = await _context.Users.ToListAsync()
            };

            return View(viewModel);
        }

        public async Task<IActionResult> Movies()
        {
            var movies = await _context.Movies
                .Include(m => m.Category)
                .ToListAsync();
            return View(movies);
        }

        public async Task<IActionResult> Series()
        {
            var series = await _context.Series
                .Include(s => s.Category)
                .ToListAsync();
            return View(series);
        }

        public async Task<IActionResult> Categories()
        {
            var categories = await _context.Categories
                .Include(c => c.Movies)
                .Include(c => c.Series)
                .ToListAsync();
            return View(categories);
        }

        public async Task<IActionResult> Users()
        {
            var users = await _context.Users.ToListAsync();
            return View(users);
        }

        // Migration işlemi
        [HttpPost]
        public IActionResult RunMigration()
        {
            var psi = new ProcessStartInfo
            {
                FileName = "dotnet",
                Arguments = "ef migrations add AdminPanelMigration_" + DateTime.Now.ToString("yyyyMMddHHmmss"),
                WorkingDirectory = _env.ContentRootPath,
                RedirectStandardOutput = true,
                RedirectStandardError = true,
                UseShellExecute = false,
                CreateNoWindow = true
            };

            using var process = Process.Start(psi);
            if (process == null)
            {
                return Content("Process başlatılamadı.");
            }

            process.WaitForExit();
            var output = process.StandardOutput.ReadToEnd();
            var error = process.StandardError.ReadToEnd();
            return Content(output + error);
        }

        // Database update işlemi
        [HttpPost]
        public IActionResult UpdateDatabase()
        {
            var psi = new ProcessStartInfo
            {
                FileName = "dotnet",
                Arguments = "ef database update",
                WorkingDirectory = _env.ContentRootPath,
                RedirectStandardOutput = true,
                RedirectStandardError = true,
                UseShellExecute = false,
                CreateNoWindow = true
            };

            using var process = Process.Start(psi);
            if (process == null)
            {
                return Content("Process başlatılamadı.");
            }

            process.WaitForExit();
            var output = process.StandardOutput.ReadToEnd();
            var error = process.StandardError.ReadToEnd();
            return Content(output + error);
        }

        // Film ekle (GET)
        public IActionResult AddMovie()
        {
            ViewBag.Categories = _context.Categories.ToList();
            return View();
        }

        // Film ekle (POST)
        [HttpPost]
        public async Task<IActionResult> AddMovie(Movie movie)
        {
            if (ModelState.IsValid)
            {
                _context.Movies.Add(movie);
                await _context.SaveChangesAsync();
                return Json(new { success = true, message = "Film başarıyla eklendi." });
            }
            ViewBag.Categories = _context.Categories.ToList();
            return Json(new { success = false, message = "Film eklenirken bir hata oluştu." });
        }

        [HttpPost]
        public async Task<IActionResult> EditMovie(Movie movie)
        {
            if (ModelState.IsValid)
            {
                _context.Movies.Update(movie);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewBag.Categories = await _context.Categories.ToListAsync();
            return View(movie);
        }

        [HttpPost]
        public async Task<IActionResult> DeleteMovie(int id)
        {
            var movie = await _context.Movies.FindAsync(id);
            if (movie != null)
            {
                _context.Movies.Remove(movie);
                await _context.SaveChangesAsync();
                return Json(new { success = true, message = "Film başarıyla silindi." });
            }
            return Json(new { success = false, message = "Film bulunamadı." });
        }

        // Dizi ekle (GET)
        public async Task<IActionResult> AddSeries()
        {
            ViewBag.Categories = await _context.Categories.ToListAsync();
            return View();
        }

        // Dizi ekle (POST)
        [HttpPost]
        public async Task<IActionResult> AddSeries(Series series)
        {
            if (ModelState.IsValid)
            {
                _context.Series.Add(series);
                await _context.SaveChangesAsync();
                return Json(new { success = true, message = "Dizi başarıyla eklendi." });
            }
            ViewBag.Categories = await _context.Categories.ToListAsync();
            return Json(new { success = false, message = "Dizi eklenirken bir hata oluştu." });
        }

        [HttpPost]
        public async Task<IActionResult> EditSeries(Series series)
        {
            if (ModelState.IsValid)
            {
                _context.Series.Update(series);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewBag.Categories = await _context.Categories.ToListAsync();
            return View(series);
        }

        // Dizi sil (POST)
        [HttpPost]
        public async Task<IActionResult> DeleteSeries(int id)
        {
            var series = await _context.Series.FindAsync(id);
            if (series != null)
            {
                _context.Series.Remove(series);
                await _context.SaveChangesAsync();
                return Json(new { success = true, message = "Dizi başarıyla silindi." });
            }
            return Json(new { success = false, message = "Dizi bulunamadı." });
        }

        [HttpPost]
        public async Task<IActionResult> DeleteUser(string id)
        {
            var user = await _userManager.FindByIdAsync(id);
            if (user != null)
            {
                var result = await _userManager.DeleteAsync(user);
                if (result.Succeeded)
                {
                    return Json(new { success = true, message = "Kullanıcı başarıyla silindi." });
                }
                return Json(new { success = false, message = "Kullanıcı silinirken bir hata oluştu." });
            }
            return Json(new { success = false, message = "Kullanıcı bulunamadı." });
        }

        [HttpPost]
        public async Task<IActionResult> SaveContent([FromBody] Movie model)
        {
            if (!ModelState.IsValid)
            {
                return Json(new { success = false, message = "Geçersiz veri." });
            }

            if (model.Id == 0)
            {
                _context.Movies.Add(model);
            }
            else
            {
                var existingMovie = await _context.Movies.FindAsync(model.Id);
                if (existingMovie == null)
                {
                    return Json(new { success = false, message = "İçerik bulunamadı." });
                }

                existingMovie.Title = model.Title;
                existingMovie.CategoryId = model.CategoryId;
                existingMovie.Description = model.Description;
            }

            await _context.SaveChangesAsync();
            return Json(new { success = true });
        }

        [HttpPost]
        public async Task<IActionResult> DeleteMultiple([FromBody] List<int> ids)
        {
            if (ids == null || !ids.Any())
            {
                return Json(new { success = false, message = "Silinecek öğe seçilmedi." });
            }

            var movies = await _context.Movies.Where(m => ids.Contains(m.Id)).ToListAsync();
            _context.Movies.RemoveRange(movies);
            await _context.SaveChangesAsync();

            return Json(new { success = true });
        }

        [HttpPost]
        public async Task<IActionResult> DeleteContent(int id)
        {
            var movie = await _context.Movies.FindAsync(id);
            if (movie == null)
            {
                return Json(new { success = false, message = "İçerik bulunamadı." });
            }

            _context.Movies.Remove(movie);
            await _context.SaveChangesAsync();

            return Json(new { success = true });
        }

        // Migration işlemi
        [HttpPost]
        public IActionResult RunMigration()
        {
            var psi = new ProcessStartInfo
            {
                FileName = "dotnet",
                Arguments = "ef migrations add AdminPanelMigration_" + DateTime.Now.ToString("yyyyMMddHHmmss"),
                WorkingDirectory = _env.ContentRootPath,
                RedirectStandardOutput = true,
                RedirectStandardError = true,
                UseShellExecute = false,
                CreateNoWindow = true
            };

            using var process = Process.Start(psi);
            if (process == null)
            {
                return Content("Process başlatılamadı.");
            }

            process.WaitForExit();
            var output = process.StandardOutput.ReadToEnd();
            var error = process.StandardError.ReadToEnd();
            return Content(output + error);
        }

        // Database update işlemi
        [HttpPost]
        public IActionResult UpdateDatabase()
        {
            var psi = new ProcessStartInfo
            {
                FileName = "dotnet",
                Arguments = "ef database update",
                WorkingDirectory = _env.ContentRootPath,
                RedirectStandardOutput = true,
                RedirectStandardError = true,
                UseShellExecute = false,
                CreateNoWindow = true
            };

            using var process = Process.Start(psi);
            if (process == null)
            {
                return Content("Process başlatılamadı.");
            }

            process.WaitForExit();
            var output = process.StandardOutput.ReadToEnd();
            var error = process.StandardError.ReadToEnd();
            return Content(output + error);
        }
    }
} 