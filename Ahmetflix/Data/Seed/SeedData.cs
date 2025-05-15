using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Ahmetflix.Models;

namespace Ahmetflix.Data
{
    public static class SeedData
    {
        public static async Task Initialize(IServiceProvider serviceProvider)
        {
            using var context = new ApplicationDbContext(
                serviceProvider.GetRequiredService<DbContextOptions<ApplicationDbContext>>());

            if (!context.Categories.Any())
            {
                var categories = new Category[]
                {
                    new Category { Name = "Aksiyon", Description = "Aksiyon filmleri ve dizileri" },
                    new Category { Name = "Komedi", Description = "Komedi filmleri ve dizileri" },
                    new Category { Name = "Drama", Description = "Drama filmleri ve dizileri" },
                    new Category { Name = "Bilim Kurgu", Description = "Bilim kurgu filmleri ve dizileri" },
                    new Category { Name = "Korku", Description = "Korku filmleri ve dizileri" },
                    new Category { Name = "Romantik", Description = "Romantik filmleri ve dizileri" },
                    new Category { Name = "Belgesel", Description = "Belgesel filmleri ve dizileri" },
                    new Category { Name = "Animasyon", Description = "Animasyon filmleri ve dizileri" },
                    new Category { Name = "Action", Description = "Action movies and series" },
                    new Category { Name = "Comedy", Description = "Comedy movies and series" },
                    new Category { Name = "Horror", Description = "Horror movies and series" },
                    new Category { Name = "Sci-Fi", Description = "Science fiction movies and series" }
                };

                await context.Categories.AddRangeAsync(categories);
                await context.SaveChangesAsync();
            }

            // Seed admin user if none exists
            var userManager = serviceProvider.GetRequiredService<UserManager<AppUser>>();
            var adminEmail = "admin@ahmetflix.com";
            var adminUser = await userManager.FindByEmailAsync(adminEmail);

            if (adminUser == null)
            {
                adminUser = new AppUser
                {
                    UserName = adminEmail,
                    Email = adminEmail,
                    EmailConfirmed = true
                };

                var result = await userManager.CreateAsync(adminUser, "Admin123!");
                if (result.Succeeded)
                {
                    await userManager.AddToRoleAsync(adminUser, "Admin");
                }
            }
        }
    }
} 