using Inventario.Frontend.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Text;

namespace Inventario.Frontend.Controllers
{
    public class ProductosController : Controller
    {
        private readonly HttpClient _httpClient;

        public ProductosController(IHttpClientFactory httpClientFactory)
        {
            _httpClient = httpClientFactory.CreateClient();
            _httpClient.BaseAddress = new Uri("https://localhost:7100/");
        }

        public async Task<IActionResult> Index()
        {
            var response = await _httpClient.GetAsync("/api/Productos");

            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                var productos = JsonConvert.DeserializeObject<IEnumerable<Producto>>(content);
                return View("Index", productos);
            }

            return View(new List<Producto>());
        }

        public IActionResult Create()
        {
           
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(Producto producto)
        {
            if (ModelState.IsValid)
            {               
                var json = JsonConvert.SerializeObject(producto);
                var content = new StringContent(json, Encoding.UTF8, "application/json");

                var response = await _httpClient.PostAsync("/api/Productos/", content);

                if (response.IsSuccessStatusCode)
                {
                    TempData["AlertMessage"] = "Producto creado exitosamente!!!";
                    return RedirectToAction("Index");
                }
                else
                {
                    TempData["ErrorMessage"] = "Error al crear el producto!!!";
                }
            }
           
            return View(producto);
        }

        public async Task<IActionResult> Edit(int id)
        {           
            var producto = await _httpClient.GetFromJsonAsync<Producto>($"/api/Productos/{id}");                                 
            return View(producto);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(int id, Producto producto)
        {
            if (ModelState.IsValid)
            {
                              
                var json = JsonConvert.SerializeObject(producto);
                var content = new StringContent(json, Encoding.UTF8, "application/json");
                
                var response = await _httpClient.PutAsync($"/api/Productos/{id}", content);

                if (response.IsSuccessStatusCode)
                {
                    TempData["AlertMessage"] = "Producto actualizado exitosamente!!!";
                    return RedirectToAction("Index");
                }
                else
                {
                    
                    TempData["ErrorMessage"] = "Error al actualizar producto!!";
                }
            }
           
            return View(producto);
        }

        public async Task<IActionResult> Entrada(int id)
        {
            var producto = await _httpClient.GetFromJsonAsync<Producto>($"/api/Productos/{id}");
            return View(producto);
        }

        [HttpPost]
        public async Task<IActionResult> Entrada(int id, Producto producto)
        {
            if (ModelState.IsValid)
            {

                producto.Stock += producto.Cantidad;
                var json = JsonConvert.SerializeObject(producto);
                var content = new StringContent(json, Encoding.UTF8, "application/json");

                var response = await _httpClient.PutAsync($"/api/Productos/{id}", content);


                if (response.IsSuccessStatusCode)
                {
                    Operacion operacion = new()
                    {
                        Tipo = "Entrada",
                        Fecha = DateTime.Now,
                        Producto = producto.Nombre,
                        Cantidad = producto.Cantidad,
                        Stock = producto.Stock,
                    };
                    var jsonOperacion = JsonConvert.SerializeObject(operacion);
                    var contentOperacion = new StringContent(jsonOperacion, Encoding.UTF8, "application/json");
                    await _httpClient.PostAsync("/api/Operaciones/", contentOperacion);
                    TempData["AlertMessage"] = "Entrada agregada exitosamente!!!";
                    return RedirectToAction("Index");
                }
                else
                {
                    TempData["ErrorMessage"] = "Error al agregar entrada!!!";
                }
            }

            return View(producto);
        }

        public async Task<IActionResult> Salida(int id)
        {
            var producto = await _httpClient.GetFromJsonAsync<Producto>($"/api/Productos/{id}");
            return View(producto);
        }

        [HttpPost]
        public async Task<IActionResult> Salida(int id, Producto producto)
        {
            if (ModelState.IsValid)
            {

                producto.Stock -= producto.Cantidad;
                var json = JsonConvert.SerializeObject(producto);
                var content = new StringContent(json, Encoding.UTF8, "application/json");

                var response = await _httpClient.PutAsync($"/api/Productos/{id}", content);


                if (response.IsSuccessStatusCode)
                {
                    Operacion operacion = new()
                    {
                        Tipo = "Salida",
                        Fecha = DateTime.Now,
                        Producto = producto.Nombre,
                        Cantidad = producto.Cantidad,
                        Stock = producto.Stock,
                    };
                    var jsonOperacion = JsonConvert.SerializeObject(operacion);
                    var contentOperacion = new StringContent(jsonOperacion, Encoding.UTF8, "application/json");
                    await _httpClient.PostAsync("/api/Operaciones/", contentOperacion);
                    TempData["AlertMessage"] = "Salida agregada exitosamente!!!";
                    return RedirectToAction("Index");
                }
                else
                {
                    TempData["ErrorMessage"] = "Error al agregar la Salida de inventario!!!";
                }
            }

            return View(producto);
        }
        public async Task<IActionResult> Delete(int id)
        {
            var response = await _httpClient.DeleteAsync($"/api/Productos/{id}");

            if (response.IsSuccessStatusCode)
            {
                TempData["AlertMessage"] = "Producto eliminado exitosamente!!!";
                return RedirectToAction("Index");
            }
            else
            {
                TempData["ErrorMessage"] = "Error al eliminar el producto.";
                return RedirectToAction("Index");
            }
        }

        public async Task<IActionResult> Details(int id)
        {

            var response = await _httpClient.GetAsync($"/api/Productos/{id}");

            if (!response.IsSuccessStatusCode)
            {
                return NotFound();
            }

            var producto = await response.Content.ReadFromJsonAsync<Producto>();
            return View(producto);
        }
    }
}
