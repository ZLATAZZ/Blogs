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



			// Add services to the container. ��������� ������� MVC
			builder.Services.AddControllersWithViews();

			var app = builder.Build();

			// Check BD connection
			// ���������, ����� �� ������������ � ��
			using (var scope = app.Services.CreateScope())
			{
				var dbContext = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();

				try
				{
					Console.WriteLine("�������� ����������� � ��...");
					if (dbContext.Database.CanConnect())
					{
						Console.WriteLine("����������� �������!");
					}
					else
					{
						Console.WriteLine("������ �����������!");
					}
				}
				catch (Exception ex)
				{
					Console.WriteLine($"������ �����������: {ex.Message}");
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
