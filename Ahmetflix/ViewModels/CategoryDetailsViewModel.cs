using Ahmetflix.Models;

namespace Ahmetflix.ViewModels
{
    public class CategoryDetailsViewModel
    {
        public Category? Category { get; set; }
        public ICollection<Movie>? Movies { get; set; }
        public ICollection<Series>? Series { get; set; }
    }
} 