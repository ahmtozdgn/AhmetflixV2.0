using System.ComponentModel.DataAnnotations;

namespace Ahmetflix.Models
{
    public class AppUser
    {
        [Key]
        public int Id { get; set; }
        public string? UserName { get; set; }
        public string? Email { get; set; }
        public string? Password { get; set; }
        public string? ImageUrl { get; set; }
        public string? Description { get; set; }
        public string? PhoneNumber { get; set; }
        public string? Address { get; set; }
        public string? Country { get; set; }
        public string? City { get; set; }

        }
}
