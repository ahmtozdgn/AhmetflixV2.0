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
    public class HomeController : Controller
    {
        private readonly ApplicationDbContext _context;

        public HomeController(ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            // Sabit örnek filmler
            var movies = new List<Movie>
            {
                new Movie { Id = 1, Title = "Interstellar", ReleaseDate = new DateTime(2014,11,7), Rating = 8.6, GenreName = "Bilim Kurgu", ImageUrl = "https://image.tmdb.org/t/p/w500/nCbkOyOMTePka7v4rWqa6hM2ksk.jpg", BackdropUrl = "https://image.tmdb.org/t/p/original/rAiYTfKGqDCRIIqo664sY9XZIvQ.jpg", Description = "Bir grup kaşif, insanlığın yıldızlar arasında hayatta kalmasını sağlamak için solucan deliğinden geçer." },
                new Movie { Id = 2, Title = "Joker", ReleaseDate = new DateTime(2019,10,4), Rating = 8.4, GenreName = "Dram", ImageUrl = "https://image.tmdb.org/t/p/w500/udDclJoHjfjb8Ekgsd4FDteOkCU.jpg", BackdropUrl = "https://image.tmdb.org/t/p/original/8QXGNP0Vb4nsYKub59XpAhiUSQN.jpg", Description = "Toplum tarafından dışlanan bir adamın Joker'e dönüşüm hikayesi." },
                new Movie { Id = 3, Title = "Inception", ReleaseDate = new DateTime(2010,7,16), Rating = 8.8, GenreName = "Aksiyon", ImageUrl = "https://image.tmdb.org/t/p/w500/edv5CZvWj09upOsy2Y6IwDhK8bt.jpg", BackdropUrl = "https://image.tmdb.org/t/p/original/s3TBrRGB1iav7gFOCNx3H31MoES.jpg", Description = "Bir hırsız, insanların rüyalarına girerek sırlarını çalmakla görevlendirilir." },
                new Movie { Id = 4, Title = "Parasite", ReleaseDate = new DateTime(2019,5,30), Rating = 8.5, GenreName = "Gerilim", ImageUrl = "https://image.tmdb.org/t/p/w500/7IiTTgloJzvGI1TAYymCfbfl3vT.jpg", BackdropUrl = "https://image.tmdb.org/t/p/original/ApiBzeaa95TNYliSbQ8pJv4Fje7.jpg", Description = "Fakir bir ailenin zengin bir ailenin evine sızma hikayesi." },
                new Movie { Id = 5, Title = "Dune", ReleaseDate = new DateTime(2021,10,22), Rating = 8.1, GenreName = "Bilim Kurgu", ImageUrl = "https://image.tmdb.org/t/p/w500/d5NXSklXo0qyIYkgV94XAgMIckC.jpg", BackdropUrl = "https://image.tmdb.org/t/p/original/8c4a8kE7PizaGQQnditMmI1xbRp.jpg", Description = "Çöl gezegeni Arrakis'te geçen destansı bir hikaye." },
                new Movie { Id = 6, Title = "Tenet", ReleaseDate = new DateTime(2020,8,26), Rating = 7.8, GenreName = "Aksiyon", ImageUrl = "https://image.tmdb.org/t/p/w500/k68nPLbIST6NP96JmTxmZijEvCA.jpg", BackdropUrl = "https://image.tmdb.org/t/p/original/6TPZSJ06OEXeelx1U1VIAt0j9Ry.jpg", Description = "Zamanı manipüle eden bir ajanın dünyayı kurtarma mücadelesi." },
                new Movie { Id = 7, Title = "The Batman", ReleaseDate = new DateTime(2022,3,4), Rating = 7.9, GenreName = "Aksiyon", ImageUrl = "https://image.tmdb.org/t/p/w500/74xTEgt7R36Fpooo50r9T25onhq.jpg", BackdropUrl = "https://image.tmdb.org/t/p/original/3WZTxpgscsmoUk81TuECXdFOD0R.jpg", Description = "Batman, Gotham'daki yolsuzluğu ve gizemli bir katili araştırıyor." },
                new Movie { Id = 8, Title = "Fight Club", ReleaseDate = new DateTime(1999,10,15), Rating = 8.8, GenreName = "Dram", ImageUrl = "https://image.tmdb.org/t/p/w500/bptfVGEQuv6vDTIMVCHjJ9Dz8PX.jpg", BackdropUrl = "https://image.tmdb.org/t/p/original/87hTDiay2N2qWyX4Ds7ybXi9h8I.jpg", Description = "Bir adamın yeraltı dövüş kulübüyle değişen hayatı." },
                new Movie { Id = 9, Title = "The Matrix", ReleaseDate = new DateTime(1999,3,31), Rating = 8.7, GenreName = "Bilim Kurgu", ImageUrl = "https://image.tmdb.org/t/p/w500/f89U3ADr1oiB1s9GkdPOEpXUk5H.jpg", BackdropUrl = "https://image.tmdb.org/t/p/original/9tgJfqk1ZQkZz1SU9vC8ENxbY1H.jpg", Description = "Gerçeklik ve simülasyon arasındaki çizgiyi sorgulatan bir başyapıt." },
                new Movie { Id = 10, Title = "Gladiator", ReleaseDate = new DateTime(2000,5,5), Rating = 8.5, GenreName = "Tarih", ImageUrl = "https://image.tmdb.org/t/p/w500/ty8TGRuvJLPUmAR1H1nRIsgwvim.jpg", BackdropUrl = "https://image.tmdb.org/t/p/original/1tC2ums3eSPVYjK6p6G6U4nQY1T.jpg", Description = "Bir Roma generalinin intikam hikayesi." }
            };
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
            var viewModel = new HomeViewModel
            {
                FeaturedMovies = movies.Take(3).ToList(),
                RecentMovies = movies,
                Categories = new List<Category> { new Category { Name = "Bilim Kurgu" }, new Category { Name = "Dram" }, new Category { Name = "Aksiyon" }, new Category { Name = "Gerilim" }, new Category { Name = "Tarih" }, new Category { Name = "Komedi" }, new Category { Name = "Fantastik" }, new Category { Name = "Gizem" } },
                SeriesList = series
            };
            return View(viewModel);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View();
        }
    }
}