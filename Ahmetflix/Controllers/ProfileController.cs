using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Ahmetflix.Data;
using Ahmetflix.Models;
using System.Linq;
using System.Threading.Tasks;

namespace Ahmetflix.Controllers
{
    [Authorize]
    public class ProfileController : Controller
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly ApplicationDbContext _context;

        public ProfileController(UserManager<AppUser> userManager, ApplicationDbContext context)
        {
            _userManager = userManager;
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null) return NotFound();

            return View(user);
        }

        [HttpPost]
        public async Task<IActionResult> UpdateProfile(AppUser model)
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null) return NotFound();

            if (ModelState.IsValid)
            {
                user.FirstName = model.FirstName;
                user.LastName = model.LastName;
                user.ProfileImageUrl = model.ProfileImageUrl;

                var result = await _userManager.UpdateAsync(user);
                if (result.Succeeded)
                {
                    return RedirectToAction(nameof(Index));
                }

                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
            }

            return View("Index", user);
        }

        [HttpGet]
        public IActionResult ChangePassword() => View();

        [HttpPost]
        public async Task<IActionResult> ChangePassword(string oldPassword, string newPassword)
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null) return RedirectToAction("Login", "Account");
            var result = await _userManager.ChangePasswordAsync(user, oldPassword, newPassword);
            if (result.Succeeded)
            {
                await _userManager.UpdateAsync(user);
                ViewBag.Message = "Şifre başarıyla değiştirildi.";
                return View();
            }
            foreach (var error in result.Errors)
                ModelState.AddModelError("", error.Description);
            return View();
        }
    }
} 