using System.ComponentModel.DataAnnotations;

namespace Ahmetflix.Models
{
    public class Category
    {
        [Key]
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? ImageUrl { get; set; }

        // Navigation Properties
        public ICollection<Movie>? Movies { get; set; }
        public ICollection<Series>? Series { get; set; }

        public List<AppUser> AppUsers { get; } = new();
        public List<IMDB> IMDBs { get; } = new();
        public List<Genre> Genres { get; } = new();
    }
}
