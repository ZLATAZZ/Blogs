using Blogs.Data;
using Blogs.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace Blogs.Controllers
{
	public class HomeController : Controller
	{
		private ApplicationDbContext _context;

		public HomeController(ApplicationDbContext context) {

			context = _context;

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

		public IActionResult Post()
		{
			return View();
		}

		[HttpGet]
		public IActionResult Edit()
		{
			return View(new Post());
		}

		[HttpPost]
		public async Task<IActionResult> Edit(Post post)
		{
			_context.Posts.Add(post);
			await _context.SaveChangesAsync();

			return RedirectToAction("Index");
		}
	}
}
