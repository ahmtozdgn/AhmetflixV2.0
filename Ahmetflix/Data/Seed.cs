using Ahmetflix.Data.Enum;
using Ahmetflix.Data;
using Ahmetflix.Models;
using Microsoft.AspNetCore.Identity;

namespace Ahmetflix.Data
{
    public class Seed
    {
        public static void SeedData(IApplicationBuilder applicationBuilder, MovieCategory movieCategory)
        {
            using (var serviceScope = applicationBuilder.ApplicationServices.CreateScope())
            {
                var context = serviceScope.ServiceProvider.GetService<ApplicationDbContext>();

                context.Database.EnsureCreated();

                if (!context.Movies.Any())
                {
                    context.Movies.AddRange(new List<Movie>()
                    {
                        new Movie()
                        {
                            Title = "Inception",
                            ImageUrl = "https://example.com/inception.jpg",
                            Description = "A mind-bending thriller by Christopher Nolan.",
                            Address = new Address()
                            {

                                City = "Los Angeles",
                            }
                        },
                        new Movie()
                        {
                            Title = "The Godfather",
                            ImageUrl = "https://example.com/godfather.jpg",
                            Description = "An iconic mafia movie directed by Francis Ford Coppola.",
                            Address = new Address()
                            {

                                City = "New York",

                            }
                        }
                    });
                    context.SaveChanges();
                }

                if (!context.Categories.Any())
                {
                    context.Categories.AddRange(new List<Category>()
                    {
                        new Category() { Name = "Drama" },
                        new Category() { Name = "Science Fiction" },
                        new Category() { Name = "Action" },
                        new Category() { Name = "Comedy" }
                    });
                    context.SaveChanges();
                }
            }
        }

        internal static void SeedData(WebApplication app)
        {
            throw new NotImplementedException();
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
        //            await userManager.AddToRoleAsync(newAdminUser, UserRoles.Admin);
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
    }
}
