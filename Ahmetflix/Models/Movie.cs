using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Ahmetflix.Models
{
    public class Movie
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "Film başlığı zorunludur.")]
        [StringLength(100, ErrorMessage = "Film başlığı en fazla 100 karakter olabilir.")]
        public string? Title { get; set; }

        [Required(ErrorMessage = "Film açıklaması zorunludur.")]
        public string? Description { get; set; }

        [Display(Name = "Yayın Tarihi")]
        [DataType(DataType.Date)]
        public DateTime? ReleaseDate { get; set; }

        [Display(Name = "Süre (Dakika)")]
        public int Duration { get; set; }

        [Display(Name = "IMDB Puanı")]
        [Range(0, 10, ErrorMessage = "IMDB puanı 0-10 arasında olmalıdır.")]
        public double Rating { get; set; }

        [Display(Name = "Film Görseli")]
        public string? ImageUrl { get; set; }

        [Display(Name = "Arkaplan Görseli")]
        public string? BackdropUrl { get; set; }

        [Display(Name = "Video URL")]
        public string? VideoUrl { get; set; }

        [Display(Name = "Fragman URL")]
        public string? TrailerUrl { get; set; }

        [Display(Name = "Yeni Mi?")]
        public bool IsNew { get; set; }

        [Display(Name = "Popüler Mi?")]
        public bool IsPopular { get; set; }

        [Display(Name = "Öne Çıkan mı?")]
        public bool IsFeatured { get; set; }

        [Display(Name = "Kategori ID")]
        public int? CategoryId { get; set; }

        [Display(Name = "Kategori")]
        public Category? Category { get; set; }

        [Display(Name = "Tür Adı")]
        public string? GenreName { get; set; }

        public IMDB? IMDB { get; set; }

        // Navigation Properties
        public ICollection<Category>? Categories { get; set; }
        public ICollection<Favorite>? Favorites { get; set; }
        public ICollection<Comment>? Comments { get; set; }
    }
}