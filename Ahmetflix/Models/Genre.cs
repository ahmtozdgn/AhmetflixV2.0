using System.ComponentModel.DataAnnotations;

namespace Ahmetflix.Models
{
    public class Genre
    {
        [Key]
        public string? Name { get; set; }
        public string? ImageUrl { get; set; }
        public string? Description { get; set; }
        public List<Movie> Movies { get; set; } = new List<Movie>();
        public List<Series> Series { get; set; } = new List<Series>();
        public List<AppUser> AppUsers { get; set; } = new List<AppUser>();
        public List<IMDB> IMDBs { get; set; } = new List<IMDB>();
        public List<Category> Categories { get; set; } = new List<Category>();
        public List<Genre> SubGenres { get; set; } = new List<Genre>();
    }
}