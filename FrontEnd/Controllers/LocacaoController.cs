using DbAccess.Models;
using FrontEnd.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Text;
using System.Text.Json;

namespace FrontEnd.Controllers
{
    public class LocacaoController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;
        public LocacaoController(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }
        // GET: LocacaoController
        public ActionResult Index()
        {
            try
            {
                CompleteDataViewModel completeDataViewModel = new CompleteDataViewModel();
                completeDataViewModel = GetCompleteDataViewModel();
                return View(completeDataViewModel);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Algo deu errado no INDEX de Locacoes"); ;
                Console.WriteLine(ex.Message);
                return View("Error");
            }
        }

        // GET: LocacaoController/Details/5
        public ActionResult Details(int id)
        {
            try
            {
                LocacaoCarroClienteViewModel locacaoCarroClienteViewModel = new LocacaoCarroClienteViewModel();
                locacaoCarroClienteViewModel = GetLocacaoCarroClienteViewModel(id);
                return View(locacaoCarroClienteViewModel);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Algo deu errado no Details de Locacoes"); ;
                Console.WriteLine(ex.Message);
                return View("Error");
            }
        }

        // GET: LocacaoController/Create
        public ActionResult Create()
        {
            try
            {
                CreateLocacaoViewModel createLocacaoViewModel = new CreateLocacaoViewModel();
                createLocacaoViewModel.Carros = GetCarros().Where(x=> x.Estado == 0).ToList();
                createLocacaoViewModel.Clientes = GetClientes();
                return View(createLocacaoViewModel);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Algo deu errado no CREATE de Locacoes"); ;
                Console.WriteLine(ex.Message);
                return View("Error");
            }
        }

        // POST: LocacaoController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                var httpClient = _httpClientFactory.CreateClient("api");
                Locacao novaLocacao = new Locacao();
                novaLocacao.ClienteId = int.Parse(collection["Locacao.ClienteId"]);
                novaLocacao.CarroId = int.Parse(collection["Locacao.CarroId"]);
                novaLocacao.DataLocacao = DateTime.Parse(collection["Locacao.DataLocacao"]);
                novaLocacao.DataDevolucao = DateTime.UnixEpoch;
                novaLocacao.ValorTotal = 0;

                string locacaoJson = JsonSerializer.Serialize<Locacao>(novaLocacao);
                var content = new StringContent(locacaoJson, Encoding.UTF8, "application/json");
                var response = httpClient.PostAsync("LocacaoAPI", content).Result;

                var res = httpClient.PutAsync($"CarroAPI/UpdateDisponibilidade/{novaLocacao.CarroId}", null).Result;

                TempData["success"] = "Locacao Cadastrada com Sucesso";
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: LocacaoController/Edit/5
        public ActionResult Edit(int id)
        {
            Locacao locacao = GetLocacao().Find(x => x.Id == id);
            return View(locacao);
        }

        // POST: LocacaoController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                var httpClient = _httpClientFactory.CreateClient("api");
                Locacao novolocacao = new Locacao();
                novolocacao.Id = int.Parse(collection["Id"]);
                novolocacao.ClienteId = int.Parse(collection["ClienteId"]);
                novolocacao.CarroId = int.Parse(collection["CarroId"]);
                novolocacao.DataLocacao = DateTime.Parse(collection["DataLocacao"]);
                novolocacao.DataDevolucao = DateTime.Parse(collection["DataDevolucao"]);
                novolocacao.ValorTotal = decimal.Parse(collection["ValorTotal"]);


                string clienteJson = JsonSerializer.Serialize<Locacao>(novolocacao);
                var content = new StringContent(clienteJson, Encoding.UTF8, "application/json");
                var response = httpClient.PutAsync($"LocacaoAPI/{id}", content).Result;
                TempData["success"] = "Locacao Editado com Sucesso";
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
            
        }

        // GET: LocacaoController/Delete/5
        public ActionResult Delete(int id)
        {
            Locacao locacao = new Locacao();
            locacao = GetLocacao().Find(x => x.Id == id);
            return View(locacao);
        }

        // POST: LocacaoController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                var httpClient = _httpClientFactory.CreateClient("api");
                var response = httpClient.DeleteAsync($"LocacaoAPI/{id}").Result;
                TempData["success"] = "Locacao Deletada com Sucesso";
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        public ActionResult Devolver(int id)
        {
            LocacaoCarroClienteViewModel locacaoCarroClienteViewModel = new LocacaoCarroClienteViewModel();
            locacaoCarroClienteViewModel = GetLocacaoCarroClienteViewModel(id);

            return View(locacaoCarroClienteViewModel);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Devolver(int id, IFormCollection collection)
        {
            try
            {
                Locacao locacao = new Locacao();
                locacao = GetLocacao().Find(x => x.Id == id);
                locacao.DataDevolucao = DateTime.Parse(collection["Locacao.DataDevolucao"]);
                decimal precoDiaria = decimal.Parse(collection["Carro.PrecoDiaria"]);
                decimal precoKM = decimal.Parse(collection["Carro.PrecoKM"]);
                int KMsRodados = int.Parse(collection["KmsRodados"]);
                locacao.ValorTotal = CalcularValorLocacao(locacao, precoDiaria, precoKM, KMsRodados);



                var httpClient = _httpClientFactory.CreateClient("api");
                string locacaoJson = JsonSerializer.Serialize<Locacao>(locacao);
                var content = new StringContent(locacaoJson, Encoding.UTF8, "application/json");
                var response2 = httpClient.PutAsync($"LocacaoAPI/{id}", content).Result;

                httpClient.PutAsync($"CarroAPI/UpdateDisponibilidade/{int.Parse(collection["Carro.Id"])}", null);
                TempData["success"] = "Locacao Devolvida com Sucesso";
                return RedirectToAction(nameof(Index));

            }
            catch (Exception ex)
            {
                Console.WriteLine("Erro Ao devolver Carro");
                Console.WriteLine(ex.Message);
                return RedirectToAction(nameof(Index));
            }
        }

        private decimal CalcularValorLocacao(Locacao locacao, decimal precoDia, decimal precoKM, int KMsRodados)
        {
            decimal valorDiarias = (locacao.DataDevolucao - locacao.DataLocacao).Days * precoDia;
            decimal valorKm = precoKM * KMsRodados ;
            return valorDiarias + valorKm;
        }

        public LocacaoCarroClienteViewModel GetLocacaoCarroClienteViewModel(int id)
        {
            LocacaoCarroClienteViewModel locacaoCarroClienteViewModel = new LocacaoCarroClienteViewModel();
            locacaoCarroClienteViewModel.Locacao = GetLocacao().Find(x => x.Id == id);
            locacaoCarroClienteViewModel.Carro = GetCarros().Find(x => x.Id == locacaoCarroClienteViewModel.Locacao.CarroId);
            locacaoCarroClienteViewModel.Cliente = GetClientes().Find(x => x.Id == locacaoCarroClienteViewModel.Locacao.ClienteId);
            return locacaoCarroClienteViewModel;
        }
        public CompleteDataViewModel GetCompleteDataViewModel()
        {
            CompleteDataViewModel completeDataViewModel = new CompleteDataViewModel();
            completeDataViewModel.Carros = GetCarros();
            completeDataViewModel.Clientes = GetClientes();
            completeDataViewModel.Locacoes = GetLocacao();
            return completeDataViewModel;
        }

        public List<Cliente> GetClientes()
        {
            List<Cliente> clientes = new List<Cliente>();
            var httpClient = _httpClientFactory.CreateClient("api");
            var response = httpClient.GetAsync("ClienteAPI").Result;
            if (response.IsSuccessStatusCode)
            {
                var json = response.Content.ReadAsStringAsync().Result;
                clientes = JsonSerializer.Deserialize<List<Cliente>>(json);
                return clientes;

            }
            else
            {
                return new List<Cliente>();
            }
        }
        public List<Carro> GetCarros()
        {
            List<Carro> carros = new List<Carro>();
            var httpClient = _httpClientFactory.CreateClient("api");
            var response =  httpClient.GetAsync("CarroAPI").Result;
            if (response.IsSuccessStatusCode)
            {
                var json = response.Content.ReadAsStringAsync().Result;
                carros = JsonSerializer.Deserialize<List<Carro>>(json);
                return carros;
            }
            else
            {
                return new List<Carro>();
            }
        }
        public List<Locacao> GetLocacao()
        {
            List<Locacao> locacoes = new List<Locacao>();
            var httpClient = _httpClientFactory.CreateClient("api");
            var response = httpClient.GetAsync("LocacaoAPI").Result;
            if (response.IsSuccessStatusCode)
            {
                var json = response.Content.ReadAsStringAsync().Result;
                locacoes = JsonSerializer.Deserialize<List<Locacao>>(json);
                return locacoes;
            }
            else
            {
                return new List<Locacao>();
            }
        }
    }
}
