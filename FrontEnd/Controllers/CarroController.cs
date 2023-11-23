using DbAccess.Models;
using Microsoft.AspNetCore.Mvc;
using System.Text;
using System.Text.Json;

namespace FrontEnd.Controllers
{
    public class CarroController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;
        public CarroController(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }
        // GET: CarroController
        public ActionResult Index()
        {
            List<Carro> carros = new List<Carro>();
            var httpClient = _httpClientFactory.CreateClient("api");
            var response = httpClient.GetAsync("CarroAPI").Result;
            if(response.IsSuccessStatusCode)
            {
                var json = response.Content.ReadAsStringAsync().Result;
                carros = JsonSerializer.Deserialize<List<Carro>>(json);
                return View(carros);
            }
            else
            {
                TempData["erro"] = "Algo De Errado aconteceu, impossível Carregar página de Carros";
                return RedirectToAction("Index", "Home");
            }
        }

        // GET: CarroController/Details/5
        public ActionResult Details(int id)
        {
            var httpClient = _httpClientFactory.CreateClient("api");
            var response = httpClient.GetAsync($"CarroAPI/{id}").Result;
            if (response.IsSuccessStatusCode)
            {
                var json = response.Content.ReadAsStringAsync().Result;
                var carro = JsonSerializer.Deserialize<Carro>(json);
                return View(carro);
            }
            else
            {
                ViewData["Mensagem"] = "Algo De Errado aconteceu, impossível Carregar página de Detalhes de Carros";
                return RedirectToAction("Index", "Home");
            }
        }

        // GET: CarroController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: CarroController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                var httpClient = _httpClientFactory.CreateClient("api");
                Carro novoCarro = new Carro();
                novoCarro.Marca = collection["Marca"];
                novoCarro.Modelo = collection["Modelo"];
                novoCarro.PrecoDiaria = Convert.ToDecimal(collection["PrecoDiaria"]);
                novoCarro.PrecoKM = Convert.ToDecimal(collection["PrecoKM"]);

                string carroJson = JsonSerializer.Serialize<Carro>(novoCarro);
                var content = new StringContent(carroJson, Encoding.UTF8, "application/json");
                var response = httpClient.PostAsync("CarroAPI", content).Result;
                TempData["success"] = "Carro Cadastrado com Sucesso";
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: CarroController/Edit/5
        public ActionResult Edit(int id)
        {
            var httpClient = _httpClientFactory.CreateClient("api");
            var response = httpClient.GetAsync($"CarroAPI/{id}").Result;
            if (response.IsSuccessStatusCode)
            {
                var json = response.Content.ReadAsStringAsync().Result;
                var carro = JsonSerializer.Deserialize<Carro>(json);
                return View(carro);
            }
            else
            {
                ViewData["Mensagem"] = "Algo De Errado aconteceu, impossível Carregar página de Editar de Carros";
                return RedirectToAction("Index", "Home");
            }
        }

        // POST: CarroController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                var httpClient = _httpClientFactory.CreateClient("api");
                Carro novoCarro = new Carro();
                novoCarro.Id = id;
                novoCarro.Marca = collection["Marca"];
                novoCarro.Modelo = collection["Modelo"];
                novoCarro.Estado = Convert.ToInt32(collection["Estado"]);
                novoCarro.PrecoDiaria = Convert.ToDecimal(collection["PrecoDiaria"]);
                novoCarro.PrecoKM = Convert.ToDecimal(collection["PrecoKM"]);

                string carroJson = JsonSerializer.Serialize<Carro>(novoCarro);
                var content = new StringContent(carroJson, Encoding.UTF8, "application/json");
                var response = httpClient.PutAsync($"CarroAPI/{id}", content).Result;
                TempData["success"] = "Carro Editado com Sucesso";
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: CarroController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: CarroController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                var httpClient = _httpClientFactory.CreateClient("api");
                var response = httpClient.DeleteAsync($"CarroAPI/{id}").Result;
                TempData["success"] = "Carro Deletado com Sucesso";
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
