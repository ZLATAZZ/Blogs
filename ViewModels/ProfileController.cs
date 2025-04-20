namespace Blogs.ViewModels
{

using Blogs.Models;
using Blogs.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

    public class ProfileController : Controller
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly ApplicationDbContext _context;

        public ProfileController(UserManager<IdentityUser> userManager, ApplicationDbContext context)
        {
            _userManager = userManager;
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
                return RedirectToAction("Login", "Auth");

            var posts = _context.Posts.Where(p => p.UserId == user.Id).ToList();

            var model = new ProfileViewModel
            {
                User = user,
                Posts = posts
            };

            return View(model); // Отображаем профиль пользователя с его постами
        }

    }

}
