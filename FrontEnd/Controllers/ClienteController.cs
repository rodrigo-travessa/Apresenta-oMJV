using DbAccess.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Text;
using System.Text.Json;

namespace FrontEnd.Controllers
{
    public class ClienteController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;
        public ClienteController(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }
        // GET: ClienteController
        public ActionResult Index()
        {
            List<Cliente> clientes = new List<Cliente>();
            var httpClient = _httpClientFactory.CreateClient("api");
            var response = httpClient.GetAsync("ClienteAPI").Result;
            if (response.IsSuccessStatusCode)
            {
                var json = response.Content.ReadAsStringAsync().Result;
                clientes = JsonSerializer.Deserialize<List<Cliente>>(json);
                return View(clientes);
            }
            else
            {
                ViewData["Mensagem"] = "Algo De Errado aconteceu, impossível Carregar página de Clientes";
                return RedirectToAction("Index", "Home");
            }
        }

        // GET: ClienteController/Details/5
        public ActionResult Details(int id)
        {
            var httpClient = _httpClientFactory.CreateClient("api");
            var response = httpClient.GetAsync($"ClienteAPI/{id}").Result;
            if (response.IsSuccessStatusCode)
            {
                var json = response.Content.ReadAsStringAsync().Result;
                var cliente = JsonSerializer.Deserialize<Cliente>(json);
                return View(cliente);
            }
            else
            {
                ViewData["Mensagem"] = "Algo De Errado aconteceu, impossível Carregar página de Detalhes de Clientes";
                return RedirectToAction("Index", "Home");
            }
        }

        // GET: ClienteController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: ClienteController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                var httpClient = _httpClientFactory.CreateClient("api");
                Cliente novoCliente = new Cliente();
                novoCliente.Nome = collection["Nome"];
                novoCliente.Sobrenome = collection["Sobrenome"];

                string clienteJson = JsonSerializer.Serialize<Cliente>(novoCliente);
                var content = new StringContent(clienteJson, Encoding.UTF8, "application/json");
                var response = httpClient.PostAsync("ClienteAPI", content).Result;
                TempData["success"] = "Cliente Cadastrado com Sucesso";
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: ClienteController/Edit/5
        public ActionResult Edit(int id)
        {
            var httpClient = _httpClientFactory.CreateClient("api");
            var response = httpClient.GetAsync($"ClienteAPI/{id}").Result;
            if (response.IsSuccessStatusCode)
            {
                var json = response.Content.ReadAsStringAsync().Result;
                var cliente = JsonSerializer.Deserialize<Cliente>(json);
                return View(cliente);
            }
            else
            {
                ViewData["Mensagem"] = "Algo De Errado aconteceu, impossível Carregar página de Editar de Clientes";
                return RedirectToAction("Index", "Home");
            }
        }

        // POST: ClienteController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                var httpClient = _httpClientFactory.CreateClient("api");
                Cliente novoCliente = new Cliente();
                novoCliente.Id = id;
                novoCliente.Nome = collection["Nome"];
                novoCliente.Sobrenome = collection["Sobrenome"];

                string clienteJson = JsonSerializer.Serialize<Cliente>(novoCliente);
                var content = new StringContent(clienteJson, Encoding.UTF8, "application/json");
                var response = httpClient.PutAsync($"ClienteAPI/{id}", content).Result;
                TempData["success"] = "Cliente Editado com Sucesso";
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: ClienteController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: ClienteController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                var httpClient = _httpClientFactory.CreateClient("api");
                var response = httpClient.DeleteAsync($"ClienteAPI/{id}").Result;
                TempData["success"] = "Cliente Deletado com Sucesso";
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
