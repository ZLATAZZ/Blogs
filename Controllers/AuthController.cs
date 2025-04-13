using Blogs.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;


namespace Blogs.Controllers
{
    public class AuthController : Controller
    {

        SignInManager<IdentityUser> _signInManager;

        public AuthController(SignInManager<IdentityUser> signInManager) {

            _signInManager = signInManager;        
        
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View(new LoginViewModel());
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel loginView) { 
        
            var result = await _signInManager.PasswordSignInAsync(loginView.UserName, loginView.Password, false, false);
            return RedirectToAction("Index", "Panel");

        }

        [HttpGet]
        public async Task<IActionResult> LogOut() { 
        
            await _signInManager.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }
    }
}
