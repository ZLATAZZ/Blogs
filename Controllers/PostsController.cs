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
                post.UserId = user.Id; // link post with current user

                
                if (string.IsNullOrWhiteSpace(post.Title) || string.IsNullOrWhiteSpace(post.Body))
                {
                    ModelState.AddModelError(string.Empty, "Title and Body are required.");
                    return View(post);  
                }

                if (post.Id > 0)
                    _repo.UpdatePost(post);
                else
                    _repo.AddPost(post);

                if (await _repo.SaveChangesAsync())
                {
                    return RedirectToAction("Index", "Profile"); // rediraction to user profile
                }
            }
            return View(post); 
        }


    }
}
