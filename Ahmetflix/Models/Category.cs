namespace Ahmetflix.Models
{
    public class Category
    {
    public int Id { get; set; }
        public string? Name { get; set; }
        public string? ImageUrl { get; set; }
        public string? Description { get; set; }
        public List<Movie> Movies { get; set; } = new List<Movie>();
        public List<Serie> Series { get; set; } = new List<Serie>();
        public List<AppUser> AppUsers { get; set; } = new List<AppUser>();
        public List<IMDB> IMDBs { get; set; } = new List<IMDB>();
        public List<Genre> Genres { get; set; } = new List<Genre>();
    }
}
