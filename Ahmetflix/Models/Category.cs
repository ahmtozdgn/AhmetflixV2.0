using System.ComponentModel.DataAnnotations;

namespace Ahmetflix.Models
{
    public class Category
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "Kategori adı zorunludur.")]
        [StringLength(50, ErrorMessage = "Kategori adı en fazla 50 karakter olabilir.")]
        [Display(Name = "Kategori Adı")]
        public string? Name { get; set; }

        [Display(Name = "Açıklama")]
        public string? Description { get; set; }

        [Display(Name = "Kategori Görseli")]
        public string? ImageUrl { get; set; }

        // Navigation Properties
        public ICollection<Movie>? Movies { get; set; }
        public ICollection<Series>? Series { get; set; }

        public List<AppUser>? AppUsers { get; } = new();
        public List<IMDB>? IMDBs { get; } = new();
        public List<Genre>? Genres { get; } = new();
    }
}