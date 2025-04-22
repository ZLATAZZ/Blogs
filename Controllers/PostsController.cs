using Blogs.Data.Repository;
using Blogs.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Blogs.Controllers
{
    [Authorize] 
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
            if (user == null)
                return RedirectToAction("Login", "Auth");

            var posts = _repo.GetAllPosts().Where(p => p.UserId == user.Id).ToList();

            ViewBag.Posts = posts;
            return View(posts);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(Post post)
        {
            var user = await _userManager.GetUserAsync(User);
            if (user != null)
            {
                post.UserId = user.Id; // Связываем пост с текущим пользователем
                if (post.Id > 0)
                    _repo.UpdatePost(post);
                else
                    _repo.AddPost(post);

                if (await _repo.SaveChangesAsync())
                {
                    return RedirectToAction("Index", "Profile"); // Редирект в профиль после создания поста
                }
            }
            return View(post);
        }

    }
}
