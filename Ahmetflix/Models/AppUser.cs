using System.ComponentModel.DataAnnotations;

namespace Ahmetflix.Models
{
    public class AppUser
    {
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? Address { get; set; }
        public string? Country { get; set; }
        public string? City { get; set; }
        public bool EmailConfirmed { get; internal set; }
    }
}