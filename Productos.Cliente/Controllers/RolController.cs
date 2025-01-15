using System.Text;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Net.Http;
using Productos.Cliente.Models;

namespace Productos.Cliente.Controllers
{
    public class RolController : Controller
    {
        private readonly HttpClient _httpClient;

        public RolController(IHttpClientFactory httpFactory)
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
            var response = await _httpClient.GetAsync("api/Roles/lista");

            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                var roles = JsonConvert.DeserializeObject<IEnumerable<RolViewModel>>(content);
                return View("Index", roles);
            }

            return View(new List<RolViewModel>());
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(RolViewModel rol)
        {
            if (ModelState.IsValid)
            {
                var json = JsonConvert.SerializeObject(rol);
                var content = new StringContent(json, Encoding.UTF8, "application/json");
                var response = await _httpClient.PostAsync("/api/Roles/crear", content);

                if (response.IsSuccessStatusCode)
                {
                    return RedirectToAction("Index");
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Error al crear rol");
                }
            }

            return View(rol);
        }

        public async Task<IActionResult> Edit(int id)
        {
            var response = await _httpClient.GetAsync($"/api/Roles/verRol?id={id}");

            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                var rol = JsonConvert.DeserializeObject<RolViewModel>(content);
                return View(rol);
            }
            else
            {
                return RedirectToAction("Details");
            }
        }

        [HttpPost]
        public async Task<IActionResult> Edit(int id, RolViewModel rol)
        {
            if (ModelState.IsValid)
            {
                var json = JsonConvert.SerializeObject(rol);
                var content = new StringContent(json, Encoding.UTF8, "application/json");

                var response = await _httpClient.PutAsync($"/api/Roles/editarRol?id={id}", content);

                if (response.IsSuccessStatusCode)
                {
                    return RedirectToAction("Index", new { id });
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Error al actualizar rol");
                }
            }

            return View(rol);
        }

        public async Task<IActionResult> Details(int id)
        {
            var response = await _httpClient.GetAsync($"api/Roles/verRol?id={id}");

            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                var rol = JsonConvert.DeserializeObject<RolViewModel>(content);
                return View(rol);
            }
            else
            {
                return RedirectToAction("Details");
            }
        }

        public async Task<IActionResult> Delete(int id)
        {
            var response = await _httpClient.DeleteAsync($"api/Roles/eliminarRol?id={id}");

            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction("Index");
            }
            else
            {
                TempData["Error"] = "Error al eliminar rol";
                return RedirectToAction("Index");
            }
        }
    }
}