using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Productos.Cliente.Models;

namespace Productos.Cliente.Controllers
{
    public class DashboardController : Controller
    {
        private readonly HttpClient _httpClient;

        public DashboardController(IHttpClientFactory httpFactory)
        {
            _httpClient = httpFactory.CreateClient();
            _httpClient.BaseAddress = new Uri("https://localhost:7207/api");
        }

        public async Task<IActionResult> Index()
        {
            var dashboardViewModel = new DashboardViewModel();

            // Obtener Roles
            var responseRoles = await _httpClient.GetAsync("/api/Roles/lista");
            if (responseRoles.IsSuccessStatusCode)
            {
                var contentRoles = await responseRoles.Content.ReadAsStringAsync();
                dashboardViewModel.Roles = JsonConvert.DeserializeObject<IEnumerable<RolViewModel>>(contentRoles);
            }

            // Obtener Productos
            var responseProductos = await _httpClient.GetAsync("/api/Productos/lista");
            if (responseProductos.IsSuccessStatusCode)
            {
                var contentProductos = await responseProductos.Content.ReadAsStringAsync();
                dashboardViewModel.Productos = JsonConvert.DeserializeObject<IEnumerable<ProductoViewModel>>(contentProductos);
            }

            return View(dashboardViewModel);
        }
    }
}