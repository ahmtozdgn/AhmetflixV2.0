using System.ComponentModel.DataAnnotations;

namespace Ahmetflix.Models
{
    public class Comment
    {
        [Key]
        public int Id { get; set; }
        public string? AppUserId { get; set; }
        public AppUser? AppUser { get; set; }
        public int? MovieId { get; set; }
        public Movie? Movie { get; set; }
        public int? SeriesId { get; set; }
        public Series? Series { get; set; }
        public string? Content { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.Now;
    }
} 