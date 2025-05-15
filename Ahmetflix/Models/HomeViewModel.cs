using System.Collections.Generic;

namespace Ahmetflix.Models
{
    public class HomeViewModel
    {
        public List<Movie>? FeaturedMovies { get; set; }
        public List<Movie>? RecentMovies { get; set; }
        public List<Category>? Categories { get; set; }
        public List<Series>? SeriesList { get; set; }
    }
} 