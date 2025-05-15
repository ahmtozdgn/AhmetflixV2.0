using Ahmetflix.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace Ahmetflix.Data
{
    public class ApplicationDbContext : IdentityDbContext<AppUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        public DbSet<Movie> Movies { get; set; }
        public DbSet<Series> Series { get; set; }

        public DbSet<Address> Addresses { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Genre> Genres { get; set; }
        public DbSet<IMDB> IMDBs { get; set; }
        public DbSet<Comment> Comments { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            
            // Configure relationships and constraints here
            builder.Entity<Comment>()
                .HasOne(c => c.AppUser)
                .WithMany(u => u.Comments)
                .HasForeignKey(c => c.AppUserId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.Entity<Comment>()
                .HasOne(c => c.Movie)
                .WithMany(m => m.Comments)
                .HasForeignKey(c => c.MovieId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.Entity<Comment>()
                .HasOne(c => c.Series)
                .WithMany(s => s.Comments)
                .HasForeignKey(c => c.SeriesId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.Entity<Movie>()
                .HasOne(m => m.Category)
                .WithMany(c => c.Movies)
                .HasForeignKey(m => m.CategoryId)
                .OnDelete(DeleteBehavior.SetNull);

            builder.Entity<Series>()
                .HasOne(s => s.Category)
                .WithMany(c => c.Series)
                .HasForeignKey(s => s.CategoryId)
                .OnDelete(DeleteBehavior.SetNull);

            builder.Entity<Movie>()
                .HasOne(m => m.IMDB)
                .WithOne(i => i.Movie)
                .HasForeignKey<IMDB>(i => i.MovieId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.Entity<Series>()
                .HasOne(s => s.IMDB)
                .WithOne(i => i.Series)
                .HasForeignKey<IMDB>(i => i.SeriesId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}