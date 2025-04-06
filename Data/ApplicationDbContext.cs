using Blogs.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;

namespace Blogs.Data
{
	public class ApplicationDbContext : DbContext
	{
		public DbSet<Post> Posts { get; set; }

		public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
		{
		}
	}
}
