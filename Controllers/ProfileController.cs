using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Blogs.Models;
using System.Linq;
using System.Threading.Tasks;
using Blogs.Data.Repository;

public class ProfileController : Controller
{
    private readonly UserManager<IdentityUser> _userManager;
    private readonly IRepository _repo;

    // Внедрение зависимостей
    public ProfileController(UserManager<IdentityUser> userManager, IRepository repo)
    {
        _userManager = userManager;
        _repo = repo;
    }

    public async Task<IActionResult> Index()
    {
        var user = await _userManager.GetUserAsync(User);
        if (user == null)
            return RedirectToAction("Login", "Auth");

        var posts = _repo.GetAllPosts().Where(p => p.UserId == user.Id).ToList();

        ViewBag.Posts = posts;
        return View(user);
    }
}
