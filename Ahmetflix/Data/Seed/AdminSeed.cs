using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Ahmetflix.Models;
using System.Threading.Tasks;

namespace Ahmetflix.Data
{
    public static class AdminSeed
    {
        public static async Task Initialize(IServiceProvider serviceProvider)
        {
            var userManager = serviceProvider.GetRequiredService<UserManager<AppUser>>();
            var roleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();

            string[] roleNames = { "Admin", "User" };
            foreach (var roleName in roleNames)
            {
                if (!await roleManager.RoleExistsAsync(roleName))
                {
                    await roleManager.CreateAsync(new IdentityRole(roleName));
                }
            }

            var adminUser = await userManager.FindByEmailAsync("admin@ahmetflix.com");
            if (adminUser == null)
            {
                adminUser = new AppUser
                {
                    UserName = "admin@ahmetflix.com",
                    Email = "admin@ahmetflix.com",
                    EmailConfirmed = true,
                    FirstName = "Admin",
                    LastName = "User"
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