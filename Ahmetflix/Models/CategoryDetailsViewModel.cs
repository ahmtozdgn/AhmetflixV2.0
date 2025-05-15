using System.Collections.Generic;

namespace Ahmetflix.Models
{
    public class CategoryDetailsViewModel
    {
        public Category? Category { get; set; }
        public List<Movie>? Movies { get; set; }
        public List<Series>? Series { get; set; }
    }
} 