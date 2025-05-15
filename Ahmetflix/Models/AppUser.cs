using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;

namespace Ahmetflix.Models
{
    public class AppUser : IdentityUser
    {
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? Address { get; set; }
        public string? City { get; set; }
        public string? Country { get; set; }
        public string? Description { get; set; }
        public string? ImageUrl { get; set; }
        public string? ProfileImageUrl { get; set; }
        public int? CategoryId { get; set; }
        public Category? Category { get; set; }
        public int? IMDBId { get; set; }
        public IMDB? IMDB { get; set; }
        public List<Comment>? Comments { get; set; }
        public string? ProfilePicture { get; set; }
        public string? Bio { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public List<Movie>? Watchlist { get; set; }
    }
}