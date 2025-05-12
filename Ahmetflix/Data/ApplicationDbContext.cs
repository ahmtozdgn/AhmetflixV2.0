using Ahmetflix.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace Ahmetflix.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        { 
        }

        public DbSet<Movie> Movies { get; set; }
        public DbSet<Series> Series { get; set; }

        public DbSet <Address> Addresses { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Genre> Genres { get; set; }
        public DbSet<AppUser> AppUsers { get; set; }
        public DbSet<IMDB> IMDBs { get; set; }

    }
}
