using System.Text;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Net.Http;
using Productos.Cliente.Models;

namespace Productos.Cliente.Controllers
{
    public class ProductoController : Controller
    {
        private readonly HttpClient _httpClient;

        public ProductoController(IHttpClientFactory httpFactory)
        {
            var handler = new HttpClientHandler();
            // Ignorar los errores de certificado (solo para pruebas, no recomendado para producción)
            handler.ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => true;

            // Crear el HttpClient con el handler configurado
            _httpClient = httpFactory.CreateClient();
            _httpClient.BaseAddress = new Uri("https://localhost:7207/api");

            // Usar el handler para configurar el cliente HTTP
            _httpClient = new HttpClient(handler)
            {
                BaseAddress = new Uri("https://localhost:7207/api")
            };
        }

        public async Task<IActionResult> Index()
        {
            var response = await _httpClient.GetAsync("api/Productos/lista");

            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                var productos = JsonConvert.DeserializeObject<IEnumerable<ProductoViewModel>>(content);
                return View("Index", productos);
            }

            return View(new List<ProductoViewModel>());
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(ProductoViewModel producto)
        {
            if (ModelState.IsValid)
            {
                var json = JsonConvert.SerializeObject(producto);
                var content = new StringContent(json, Encoding.UTF8, "application/json");
                var response = await _httpClient.PostAsync("/api/Productos/crear", content);

                if (response.IsSuccessStatusCode)
                {
                    return RedirectToAction("Index");
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Error al crear producto");
                }
            }

            return View(producto);
        }

        public async Task<IActionResult> Edit(int id)
        {
            var response = await _httpClient.GetAsync($"/api/Productos/verProducto?id={id}");

            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                var producto = JsonConvert.DeserializeObject<ProductoViewModel>(content);
                return View(producto);
            }
            else
            {
                return RedirectToAction("Details");
            }
        }

        [HttpPost]
        public async Task<IActionResult> Edit(int id, ProductoViewModel producto)
        {
            if (ModelState.IsValid)
            {
                var json = JsonConvert.SerializeObject(producto);
                var content = new StringContent(json, Encoding.UTF8, "application/json");

                var response = await _httpClient.PutAsync($"/api/Productos/editarProducto?id={id}", content);

                if (response.IsSuccessStatusCode)
                {
                    return RedirectToAction("Index", new { id });
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Error al actualizar producto");
                }
            }

            return View(producto);
        }

        public async Task<IActionResult> Details(int id)
        {
            var response = await _httpClient.GetAsync($"api/Productos/verProducto?id={id}");

            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                var producto = JsonConvert.DeserializeObject<ProductoViewModel>(content);
                return View(producto);
            }
            else
            {
                return RedirectToAction("Details");
            }
        }

        public async Task<IActionResult> Delete(int id)
        {
            var response = await _httpClient.DeleteAsync($"api/Productos/eliminarProducto?id={id}");

            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction("Index");
            }
            else
            {
                TempData["Error"] = "Error al eliminar producto";
                return RedirectToAction("Index");
            }
        }
    }
}