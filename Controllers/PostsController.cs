using Blogs.Data.Repository;
using Blogs.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Blogs.Controllers
{
    [Authorize] // Только авторизованные могут создавать посты
    public class PostsController : Controller
    {
        private readonly IRepository _repo;
        private readonly UserManager<IdentityUser> _userManager;

        public PostsController(IRepository repo, UserManager<IdentityUser> userManager)
        {
            _repo = repo;
            _userManager = userManager;
        }

        public async Task<IActionResult> Index()
        {
            var user = await _userManager.GetUserAsync(User);
            var posts = _repo.GetAllPosts().Where(p => p.UserId == user.Id).ToList(); // Только свои посты
            return View(posts);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Post post)
        {
            if (ModelState.IsValid)
            {
                var user = await _userManager.GetUserAsync(User);
                post.UserId = user.Id;

                _repo.AddPost(post);
                return RedirectToAction(nameof(Index));
            }
            return View(post);
        }
    }
}
