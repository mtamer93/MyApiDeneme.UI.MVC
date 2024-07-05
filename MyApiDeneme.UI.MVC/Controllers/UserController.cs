using Microsoft.AspNetCore.Mvc;
using MyApiDeneme.UI.MVC.Models;
using MyApiDeneme.UI.MVC.Services;

namespace MyApiDeneme.UI.MVC.Controllers
{
    public class UserController : Controller
    {
        private readonly ApiClient _apiClient;
        private readonly ILogger<UserController> _logger;

        public UserController(ApiClient apiClient, ILogger<UserController> logger)
        {
            _apiClient = apiClient;
            _logger = logger;
        }

        [HttpGet]
        public IActionResult Register()
        {
            return View(new Users());
        }

        [HttpPost]
        public async Task<IActionResult> Register(Users user)
        {
            if (!ModelState.IsValid)
            {
                return View(user); // Hatalı model durumunda model ile birlikte geri dön
            }

            var success = await _apiClient.RegisterUserAsync(user);

            if (success)
            {
                return RedirectToAction("Index", "Home");
            }
            else
            {
                _logger.LogError("User registration failed for {Username}", user.Username);
                ModelState.AddModelError("", "Registration failed. Please try again.");
                return View(user); // Başarısızlık durumunda model ile birlikte geri dön
            }
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View(new Users());
        }

        [HttpPost]
        public async Task<IActionResult> Login(Users user)
        {
            if (!ModelState.IsValid)
            {
                return View(user); // Hatalı model durumunda model ile birlikte geri dön
            }

            var success = await _apiClient.LoginUserAsync(user);

            if (success)
            {
                return RedirectToAction("Index", "Home");
            }
            else
            {
                _logger.LogError("User login failed for {Username}", user.Username);
                ModelState.AddModelError("", "Login failed. Please try again.");
                return View(user); // Başarısızlık durumunda model ile birlikte geri dön
            }
        }
    }
}
