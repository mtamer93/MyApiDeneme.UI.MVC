using MyApiDeneme.UI.MVC.Models;

namespace MyApiDeneme.UI.MVC.Services
{
    public class ApiClient
    {
        private readonly HttpClient _httpClient;
        private readonly ILogger<ApiClient> _logger;

        public ApiClient(HttpClient httpClient, ILogger<ApiClient> logger)
        {
            _httpClient = httpClient;
            _logger = logger;
        }

        public async Task<bool> RegisterUserAsync(Users user)
        {
            try
            {
                var response = await _httpClient.PostAsJsonAsync("api/Auth/register", user);

                if (!response.IsSuccessStatusCode)
                {
                    var errorContent = await response.Content.ReadAsStringAsync();
                    _logger.LogError("Register failed: {StatusCode} - {ErrorContent}", response.StatusCode, errorContent);
                    return false;
                }

                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while registering the user.");
                return false;
            }
        }

        public async Task<bool> LoginUserAsync(Users user)
        {
            try
            {
                var response = await _httpClient.PostAsJsonAsync("api/Auth/login", user);

                if (!response.IsSuccessStatusCode)
                {
                    var errorContent = await response.Content.ReadAsStringAsync();
                    _logger.LogError("Login failed: {StatusCode} - {ErrorContent}", response.StatusCode, errorContent);
                    return false;
                }

                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while logging in the user.");
                return false;
            }
        }

        public async Task<List<Products>> GetProductsAsync()
        {
            try
            {
                var response = await _httpClient.GetAsync("api/products");

                if (response.IsSuccessStatusCode)
                {
                    return await response.Content.ReadFromJsonAsync<List<Products>>();
                }
                else
                {
                    var errorContent = await response.Content.ReadAsStringAsync();
                    _logger.LogError("Get products failed: {StatusCode} - {ErrorContent}", response.StatusCode, errorContent);
                    return null;
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while getting products.");
                return null;
            }
        }
    }
}
