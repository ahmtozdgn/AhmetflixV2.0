using Ahmetflix.Data;
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
                var firstCategory = context.Categories.First(); // İlk kategoriye bağla

                var movies = new List<Movie>
                {
                    new Movie
                    {
                        Title = "Inception",
                        Description = "A mind-bending thriller by Christopher Nolan.",
                        ImageUrl = "https://example.com/inception.jpg",
                        ReleaseDate = new DateTime(2010, 7, 16),
                        Duration = 148,
                        Rating = 8.8,
                        TrailerUrl = "https://youtube.com/example_trailer",
                        CategoryId = firstCategory.Id,
                        Address = new Address { City = "Los Angeles" }
                    },
                    new Movie
                    {
                        Title = "The Dark Knight",
                        Description = "Batman faces Joker in Gotham.",
                        ImageUrl = "https://example.com/darkknight.jpg",
                        ReleaseDate = new DateTime(2008, 7, 18),
                        Duration = 152,
                        Rating = 9.0,
                        TrailerUrl = "https://youtube.com/darkknight_trailer",
                        CategoryId = firstCategory.Id,
                        Address = new Address { City = "Gotham" }
                    }
                };

                context.Movies.AddRange(movies);
                context.SaveChanges(); // Filmler kaydedildi
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
    