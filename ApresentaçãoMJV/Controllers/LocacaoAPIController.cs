using DbAccess.Models;
using DbAccess.Repository.IRepository;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ApresentaçãoMJV.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LocacaoAPIController : ControllerBase
    {
        private readonly ILocacaoRepository _locacaoRepository;

        public LocacaoAPIController(ILocacaoRepository locacaoRepository)
        {
            _locacaoRepository = locacaoRepository;
        }

        // GET: api/<locacaoAPIController>
        [HttpGet]
        public ActionResult<IEnumerable<Locacao>> GetAll()
        {
            IEnumerable<Locacao> locacoes = new List<Locacao>();
            try
            {
                locacoes = _locacaoRepository.GetAll().ToList();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Erro na API GET locacoes");
                Console.WriteLine(ex.Message);
            }
            return Ok(locacoes);
        }

        // GET api/<locacaoAPIController>/5
        [HttpGet("{id}")]
        public ActionResult<Locacao> Get(int id)
        {
            Locacao locacao = new Locacao();
            try
            {
                locacao = _locacaoRepository.Get(id);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Erro na API GET Locacao");
                Console.WriteLine(ex.Message);
            }
            return Ok(locacao);
        }

        // POST api/<locacaoAPIController>
        [HttpPost]
        public void Post([FromBody] Locacao locacao)
        {
            try
            {
                _locacaoRepository.Add(locacao);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Erro na API Add Locacao");
                Console.WriteLine(ex.Message);
            }
        }

        // PUT api/<locacaoAPIController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] Locacao locacao)
        {
            try
            {
                _locacaoRepository.Update(locacao);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Erro na API Update Locacao");
                Console.WriteLine(ex.Message);
            }
        }

        // DELETE api/<locacaoAPIController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            try
            {
                _locacaoRepository.Delete(id);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Erro na API Delete Locacao");
                Console.WriteLine(ex.Message);
            }
        }
    }
}
