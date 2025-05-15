using System.Collections.Generic;
using Microsoft.AspNetCore.Identity;

namespace Ahmetflix.Models
{
    public class AdminViewModel
    {
        public List<Movie>? Movies { get; set; }
        public List<Series>? Series { get; set; }
        public List<Category>? Categories { get; set; }
        public List<AppUser>? Users { get; set; }
    }
} 