using Blogs.Data;
using Blogs.Data.Repository;
using Blogs.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace Blogs.Controllers
{
	public class HomeController : Controller
	{
		private IRepository _repo;

		public HomeController(IRepository repo) {

			_repo = repo;

		}

		public IActionResult Index()
		{
			
			return View();
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

		public IActionResult Post(int id)
		{

			var post = _repo.GetPost(id);

			return View(post);
		}
        public IActionResult Posts()
        {
            var posts = _repo.GetAllPosts();
            return View(posts);
        }
    }
}
