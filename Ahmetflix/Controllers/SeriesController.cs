using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Ahmetflix.Data;
using Ahmetflix.Models;
using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;
using System;

namespace Ahmetflix.Controllers
{
    public class SeriesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public SeriesController(ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult Index(string? search, int? categoryId)
        {
            // Sabit örnek diziler
            var series = new List<Series>
            {
                new Series { Id = 1, Title = "Stranger Things", ReleaseDate = new DateTime(2016,7,15), Rating = 8.7, GenreName = "Bilim Kurgu", ImageUrl = "https://image.tmdb.org/t/p/w500/x2LSRK2Cm7MZhjluni1msVJ3wDF.jpg", Description = "Küçük bir kasabada kaybolan bir çocuğun ardından gelişen doğaüstü olaylar." },
                new Series { Id = 2, Title = "The Boys", ReleaseDate = new DateTime(2019,7,26), Rating = 8.7, GenreName = "Aksiyon", ImageUrl = "https://image.tmdb.org/t/p/w500/mY7SeH4HFFxW1hiI6cWuwCRKptN.jpg", Description = "Süper kahramanların yozlaşmış olduğu bir dünyada adalet arayan bir grup insan." },
                new Series { Id = 3, Title = "Dark", ReleaseDate = new DateTime(2017,12,1), Rating = 8.8, GenreName = "Gizem", ImageUrl = "https://image.tmdb.org/t/p/w500/apbrbWs8M9lyOpJYU5WXrpFbk1Z.jpg", Description = "Bir Alman kasabasında geçen zaman yolculuğu ve gizem dolu olaylar." },
                new Series { Id = 4, Title = "Breaking Bad", ReleaseDate = new DateTime(2008,1,20), Rating = 9.5, GenreName = "Dram", ImageUrl = "https://image.tmdb.org/t/p/w500/ggFHVNu6YYI5L9pCfOacjizRGt.jpg", Description = "Bir kimya öğretmeninin uyuşturucu imparatorluğuna dönüşümü." },
                new Series { Id = 5, Title = "The Office", ReleaseDate = new DateTime(2005,3,24), Rating = 8.9, GenreName = "Komedi", ImageUrl = "https://image.tmdb.org/t/p/w500/qWnJzyZhyy74gjpSjIXWmuk0ifX.jpg", Description = "Bir ofiste geçen komik ve absürt olaylar." },
                new Series { Id = 6, Title = "Chernobyl", ReleaseDate = new DateTime(2019,5,6), Rating = 9.4, GenreName = "Tarih", ImageUrl = "https://image.tmdb.org/t/p/w500/hlLXt2tOPT6RRnjiUmoxyG1LTFi.jpg", Description = "Çernobil nükleer felaketinin dramatik anlatımı." },
                new Series { Id = 7, Title = "Sherlock", ReleaseDate = new DateTime(2010,7,25), Rating = 9.1, GenreName = "Gizem", ImageUrl = "https://image.tmdb.org/t/p/w500/f9zGxLHGyQB10cMDZNY5ZcGKhZi.jpg", Description = "Modern Londra'da geçen Sherlock Holmes hikayeleri." },
                new Series { Id = 8, Title = "Loki", ReleaseDate = new DateTime(2021,6,9), Rating = 8.2, GenreName = "Fantastik", ImageUrl = "https://image.tmdb.org/t/p/w500/voHUmluYmKyleFkTu3lOXQG702u.jpg", Description = "Marvel evreninin en yaramaz tanrısı Loki'nin maceraları." },
                new Series { Id = 9, Title = "Wednesday", ReleaseDate = new DateTime(2022,11,23), Rating = 8.4, GenreName = "Komedi", ImageUrl = "https://image.tmdb.org/t/p/w500/9PFonBhy4cQy7Jz20NpMygczOkv.jpg", Description = "Addams ailesinin kızı Wednesday'in okul maceraları." },
                new Series { Id = 10, Title = "The Crown", ReleaseDate = new DateTime(2016,11,4), Rating = 8.6, GenreName = "Tarih", ImageUrl = "https://image.tmdb.org/t/p/w500/ltjUOqQK6f0Hk9hW5ZpLkK7fKSD.jpg", Description = "İngiliz kraliyet ailesinin yaşamı ve entrikaları." }
            };
            ViewBag.Categories = new List<Category> { new Category { Name = "Bilim Kurgu" }, new Category { Name = "Dram" }, new Category { Name = "Aksiyon" }, new Category { Name = "Gizem" }, new Category { Name = "Komedi" }, new Category { Name = "Tarih" }, new Category { Name = "Fantastik" } };
            return View(series);
        }

        public async Task<IActionResult> Details(int id)
        {
            var series = await _context.Series
                .Include(s => s.Category)
                .FirstOrDefaultAsync(s => s.Id == id);

            if (series == null)
            {
                return NotFound();
            }

            return View(series);
        }

        [Authorize]
        public async Task<IActionResult> Watch(int id)
        {
            var series = await _context.Series.FindAsync(id);
            if (series == null)
            {
                return NotFound();
            }

            return View(series);
        }
    }
}