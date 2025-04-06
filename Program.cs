using Blogs.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.AspNetCore.Identity;

namespace Blogs
{
	public class Program
	{
		public static void Main(string[] args)
		{
			var builder = WebApplication.CreateBuilder(args);

			builder.Services.AddDbContext<ApplicationDbContext>(options =>
			options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));



			// Add services to the container. Добавляем сервисы MVC
			builder.Services.AddControllersWithViews();

			var app = builder.Build();

			// Check BD connection
			// Проверяем, можно ли подключиться к БД
			using (var scope = app.Services.CreateScope())
			{
				var dbContext = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();

				try
				{
					Console.WriteLine("Проверка подключения к БД...");
					if (dbContext.Database.CanConnect())
					{
						Console.WriteLine("Подключение успешно!");
					}
					else
					{
						Console.WriteLine("Ошибка подключения!");
					}
				}
				catch (Exception ex)
				{
					Console.WriteLine($"Ошибка подключения: {ex.Message}");
				}
			}

			// Configure the HTTP request pipeline.
			if (!app.Environment.IsDevelopment())
			{
				app.UseExceptionHandler("/Home/Error");
				// The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
				app.UseHsts();
			}


			app.UseHttpsRedirection();
			app.UseStaticFiles();

			app.UseRouting();

			app.UseAuthorization();

			app.MapControllerRoute(
				name: "default",
				pattern: "{controller=Home}/{action=Index}/{id?}");

			app.Run();
		}
	}
}
