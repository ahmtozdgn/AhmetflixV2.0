using Ahmetflix.Models;

namespace Ahmetflix.Data.Seed
{
    public static class SeedData
    {
        internal static void SeedDatabase(WebApplication app)
        {
            using var scope = app.Services.CreateScope();
            var context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();

            // Eğer veritabanında hiç Category yoksa ekle
            if (!context.Categories.Any())
            {
                var categories = new List<Category>
                {
                    new Category { Name = "Action" },
                    new Category { Name = "Comedy" },
                    new Category { Name = "Drama" },
                    new Category { Name = "Sci-Fi" }
                };
                context.Categories.AddRange(categories);
                context.SaveChanges(); // Kategoriler kaydedildi
            }

            // Eğer veritabanında hiç Movie yoksa ekle
            if (!context.Movies.Any())
            {
                var categories = context.Categories.ToList();
                var movies = new List<Movie>
                {
                    new Movie { Title = "Inception", Description = "A mind-bending thriller by Christopher Nolan.", ImageUrl = "https://image.tmdb.org/t/p/w500/qmDpIHrmpJINaRKAfWQfftjCdyi.jpg", ReleaseDate = new DateTime(2010, 7, 16), Duration = 148, Rating = 8.8, TrailerUrl = "https://www.youtube.com/watch?v=YoHD9XEInc0", CategoryId = categories[0].Id },
                    new Movie { Title = "The Dark Knight", Description = "Batman faces Joker in Gotham.", ImageUrl = "https://image.tmdb.org/t/p/w500/1hRoyzDtpgMU7Dz4JF22RANzQO7.jpg", ReleaseDate = new DateTime(2008, 7, 18), Duration = 152, Rating = 9.0, TrailerUrl = "https://www.youtube.com/watch?v=EXeTwQWrcwY", CategoryId = categories[0].Id },
                    new Movie { Title = "Interstellar", Description = "A team travels through a wormhole in space.", ImageUrl = "https://image.tmdb.org/t/p/w500/rAiYTfKGqDCRIIqo664sY9XZIvQ.jpg", ReleaseDate = new DateTime(2014, 11, 7), Duration = 169, Rating = 8.6, TrailerUrl = "https://www.youtube.com/watch?v=zSWdZVtXT7E", CategoryId = categories[3].Id },
                    new Movie { Title = "Forrest Gump", Description = "The story of Forrest Gump.", ImageUrl = "https://image.tmdb.org/t/p/w500/saHP97rTPS5eLmrLQEcANmKrsFl.jpg", ReleaseDate = new DateTime(1994, 7, 6), Duration = 142, Rating = 8.8, TrailerUrl = "https://www.youtube.com/watch?v=bLvqoHBptjg", CategoryId = categories[2].Id },
                    new Movie { Title = "The Matrix", Description = "A computer hacker learns about the true nature of reality.", ImageUrl = "https://image.tmdb.org/t/p/w500/f89U3ADr1oiB1s9GkdPOEpXUk5H.jpg", ReleaseDate = new DateTime(1999, 3, 31), Duration = 136, Rating = 8.7, TrailerUrl = "https://www.youtube.com/watch?v=vKQi3bBA1y8", CategoryId = categories[3].Id },
                    new Movie { Title = "Pulp Fiction", Description = "The lives of two mob hitmen.", ImageUrl = "https://image.tmdb.org/t/p/w500/dM2w364MScsjFf8pfMbaWUcWrR.jpg", ReleaseDate = new DateTime(1994, 10, 14), Duration = 154, Rating = 8.9, TrailerUrl = "https://www.youtube.com/watch?v=s7EdQ4FqbhY", CategoryId = categories[1].Id },
                    new Movie { Title = "Fight Club", Description = "An insomniac office worker crosses paths with a soap maker.", ImageUrl = "https://image.tmdb.org/t/p/w500/bptfVGEQuv6vDTIMVCHjJ9Dz8PX.jpg", ReleaseDate = new DateTime(1999, 10, 15), Duration = 139, Rating = 8.8, TrailerUrl = "https://www.youtube.com/watch?v=SUXWAEX2jlg", CategoryId = categories[2].Id },
                    new Movie { Title = "The Godfather", Description = "The aging patriarch of an organized crime dynasty.", ImageUrl = "https://image.tmdb.org/t/p/w500/3bhkrj58Vtu7enYsRolD1fZdja1.jpg", ReleaseDate = new DateTime(1972, 3, 24), Duration = 175, Rating = 9.2, TrailerUrl = "https://www.youtube.com/watch?v=sY1S34973zA", CategoryId = categories[2].Id },
                    new Movie { Title = "Back to the Future", Description = "Marty McFly travels back in time.", ImageUrl = "https://image.tmdb.org/t/p/w500/pTpxQB1N0waaSc3OSn0e9oc8kx9.jpg", ReleaseDate = new DateTime(1985, 7, 3), Duration = 116, Rating = 8.5, TrailerUrl = "https://www.youtube.com/watch?v=qvsgGtivCgs", CategoryId = categories[3].Id },
                    new Movie { Title = "The Shawshank Redemption", Description = "Two imprisoned men bond over a number of years.", ImageUrl = "https://image.tmdb.org/t/p/w500/q6y0Go1tsGEsmtFryDOJo3dEmqu.jpg", ReleaseDate = new DateTime(1994, 9, 23), Duration = 142, Rating = 9.3, TrailerUrl = "https://www.youtube.com/watch?v=6hB3S9bIaco", CategoryId = categories[2].Id }
                };
                context.Movies.AddRange(movies);
                context.SaveChanges();
            }

            // Eğer veritabanında hiç Dizi yoksa ekle
            if (!context.Series.Any())
            {
                var categories = context.Categories.ToList();
                var seriesList = new List<Series>
                {
                    new Series { Title = "Breaking Bad", Description = "A high school chemistry teacher turned methamphetamine producer.", ImageUrl = "https://image.tmdb.org/t/p/w500/ggFHVNu6YYI5L9pCfOacjizRGt.jpg", ReleaseDate = new DateTime(2008, 1, 20), Season = "5", Duration = 49, Rating = 9.5, CategoryId = categories[2].Id },
                    new Series { Title = "Stranger Things", Description = "A group of kids uncover supernatural mysteries.", ImageUrl = "https://image.tmdb.org/t/p/w500/x2LSRK2Cm7MZhjluni1msVJ3wDF.jpg", ReleaseDate = new DateTime(2016, 7, 15), Season = "4", Duration = 51, Rating = 8.7, CategoryId = categories[3].Id },
                    new Series { Title = "Game of Thrones", Description = "Noble families vie for control of the Iron Throne.", ImageUrl = "https://image.tmdb.org/t/p/w500/u3bZgnGQ9T01sWNhyveQz0wH0Hl.jpg", ReleaseDate = new DateTime(2011, 4, 17), Season = "8", Duration = 57, Rating = 9.3, CategoryId = categories[2].Id },
                    new Series { Title = "The Office", Description = "A mockumentary on a group of typical office workers.", ImageUrl = "https://image.tmdb.org/t/p/w500/qWnJzyZhyy74gjpSjIXWmuk0ifX.jpg", ReleaseDate = new DateTime(2005, 3, 24), Season = "9", Duration = 22, Rating = 8.9, CategoryId = categories[1].Id },
                    new Series { Title = "Friends", Description = "Follows the personal and professional lives of six friends.", ImageUrl = "https://image.tmdb.org/t/p/w500/f496cm9enuEsZkSPzCwnTESEK5s.jpg", ReleaseDate = new DateTime(1994, 9, 22), Season = "10", Duration = 22, Rating = 8.8, CategoryId = categories[1].Id },
                    new Series { Title = "The Mandalorian", Description = "A lone gunfighter makes his way through the outer reaches of the galaxy.", ImageUrl = "https://image.tmdb.org/t/p/w500/sWgBv7LV2PRoQgkxwlibdGXKz1S.jpg", ReleaseDate = new DateTime(2019, 11, 12), Season = "3", Duration = 35, Rating = 8.7, CategoryId = categories[0].Id },
                    new Series { Title = "Sherlock", Description = "A modern update finds the famous sleuth and his doctor partner solving crime.", ImageUrl = "https://image.tmdb.org/t/p/w500/f9zGxLHGyQB10cMDZNY5ZcGKhZi.jpg", ReleaseDate = new DateTime(2010, 7, 25), Season = "4", Duration = 88, Rating = 9.1, CategoryId = categories[2].Id },
                    new Series { Title = "The Crown", Description = "Follows the political rivalries and romance of Queen Elizabeth II's reign.", ImageUrl = "https://image.tmdb.org/t/p/w500/ltz4JfWb1x2h2T5D8FzQF7Qn2Qb.jpg", ReleaseDate = new DateTime(2016, 11, 4), Season = "5", Duration = 58, Rating = 8.7, CategoryId = categories[2].Id },
                    new Series { Title = "Rick and Morty", Description = "An animated series that follows the exploits of a super scientist and his not-so-bright grandson.", ImageUrl = "https://image.tmdb.org/t/p/w500/8kOWDBK6XlPUzckuHDo3wwVRFwt.jpg", ReleaseDate = new DateTime(2013, 12, 2), Season = "6", Duration = 23, Rating = 9.2, CategoryId = categories[1].Id },
                    new Series { Title = "Westworld", Description = "A dark odyssey about the dawn of artificial consciousness.", ImageUrl = "https://image.tmdb.org/t/p/w500/y55oBgf6bVMI7sFNXwJDrSIxPQt.jpg", ReleaseDate = new DateTime(2016, 10, 2), Season = "4", Duration = 62, Rating = 8.6, CategoryId = categories[3].Id }
                };
                context.Series.AddRange(seriesList);
                context.SaveChanges();
            }
        }
    }
}

//public static async Task SeedUsersAndRolesAsync(IApplicationBuilder applicationBuilder)
//{
//    using (var serviceScope = applicationBuilder.ApplicationServices.CreateScope())
//    {
//        var roleManager = serviceScope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();

//        if (!await roleManager.RoleExistsAsync(UserRoles.Admin))
//            await roleManager.CreateAsync(new IdentityRole(UserRoles.Admin));
//        if (!await roleManager.RoleExistsAsync(UserRoles.User))
//            await roleManager.CreateAsync(new IdentityRole(UserRoles.User));

//        var userManager = serviceScope.ServiceProvider.GetRequiredService<UserManager<AppUser>>();
//        string adminUserEmail = "admin@filmsite.com";

//        var adminUser = await userManager.FindByEmailAsync(adminUserEmail);
//        if (adminUser == null)
//        {
//            var newAdminUser = new AppUser()
//            {
//                UserName = "adminuser",
//                Email = adminUserEmail,
//                EmailConfirmed = true,
//                Address = new Address()
//                {
//                    Street = "789 Admin Blvd",
//                    City = "Los Angeles",
//                    State = "CA"
//                }
//            };
//            await userManager.CreateAsync(newAdminUser, "FilmSite@123");
//            object adminRoleAssignment = await userManager.AddToRoleAsync(newAdminUser,
//                                                                          UserRoles.Admin);
//        }

//        string appUserEmail = "user@filmsite.com";

//        var appUser = await userManager.FindByEmailAsync(appUserEmail);
//        if (appUser == null)
//        {
//            var newAppUser = new AppUser()
//            {
//                UserName = "movie-fan",
//                Email = appUserEmail,
//                EmailConfirmed = true,
//                Address = new Address()
//                {
//                    Street = "321 User Ln",
//                    City = "Miami",
//                    State = "FL"
//                }
//            };
//            await userManager.CreateAsync(newAppUser, "FilmSite@123");
//            await userManager.AddToRoleAsync(newAppUser, UserRoles.User);
//        }
//    }
//}