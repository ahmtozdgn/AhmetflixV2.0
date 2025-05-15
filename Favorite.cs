     public class Favorite
     {
         [Key]
         public int Id { get; set; }
         public string? AppUserId { get; set; }
         public int? MovieId { get; set; }
         public Movie? Movie { get; set; }
         public int? SeriesId { get; set; }
         public Series? Series { get; set; }
    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        builder.Entity<Favorite>()
            .HasOne(f => f.Movie)
            .WithMany(m => m.Favorites)
            .HasForeignKey(f => f.MovieId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.Entity<Favorite>()
            .HasOne(f => f.Series)
            .WithMany(s => s.Favorites)
            .HasForeignKey(f => f.SeriesId)
            .OnDelete(DeleteBehavior.Restrict);
    }
     }
     