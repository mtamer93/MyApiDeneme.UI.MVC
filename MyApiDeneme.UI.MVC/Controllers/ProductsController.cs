using Microsoft.AspNetCore.Mvc;
using MyApiDeneme.UI.MVC.Services;

namespace MyApiDeneme.UI.MVC.Controllers
{
    public class ProductsController : Controller
    {
        private readonly ApiClient _apiClient;
        private readonly ILogger<ProductsController> _logger;

        public ProductsController(ApiClient apiClient, ILogger<ProductsController> logger)
        {
            _apiClient = apiClient;
            _logger = logger;
        }

        public async Task<IActionResult> Index()
        {
            var products = await _apiClient.GetProductsAsync();

            if (products == null)
            {
                _logger.LogError("Failed to load products.");
                return View("Error"); // Hata durumunda bir hata sayfasına yönlendirin
            }

            return View(products);
        }
    }
}
