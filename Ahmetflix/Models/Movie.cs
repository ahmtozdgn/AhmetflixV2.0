using System.ComponentModel.DataAnnotations;

namespace Ahmetflix.Models
{
    public class Movie
    {
        [Key]
        public int Id { get; set; }
        public string? Title { get; set; }
        public string? Description { get; set; }
        public string? ImageUrl { get; set; }
        public string? TrailerUrl { get; set; }
        public DateTime ReleaseDate { get; set; }
        public int Duration { get; set; } // in minutes
        public double Rating { get; set; } // 0-10
        public int CategoryId { get; set; }
        public Category? Category { get; set; }
        public List<AppUser> AppUsers { get; set; } = new List<AppUser>();
        public IMDB? IMDB { get; set; }
    }
}
