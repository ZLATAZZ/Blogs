using Blogs.Data.Repository;
using Blogs.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace Blogs.Controllers
{
    public class PanelController : Controller
    {
        private IRepository _repo;

        public PanelController(IRepository repo) {

            _repo = repo;

        }

        public IActionResult Index()
        {
            var posts = _repo.GetAllPosts();
            return View(posts);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }


        [HttpGet]
        public IActionResult Edit(int? id)
        {
            if (id == null)
                return View(new Post());
            else
            {
                var post = _repo.GetPost((int)id);
                return View(post);
            }

        }

        [HttpPost]
        public async Task<IActionResult> Edit(Post post)
        {
            Console.WriteLine($"Title: {post.Title}, Body: {post.Body}"); // Выводим полученные данные
            if (post.Id > 0)
                _repo.UpdatePost(post);
            else
                _repo.AddPost(post);

            if (await _repo.SaveChangesAsync())
                return RedirectToAction("Posts", "Home");

            else
                return View(post); // Если ошибка, возвращаем модель с данными в представление
        }

        [HttpGet]
        public async Task<IActionResult> Remove(int id)
        {
            _repo.RemovePost(id);
            await _repo.SaveChangesAsync();
            return RedirectToAction("Index", "Profile");

        }
    }
}
