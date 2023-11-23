using DbAccess.Models;
using DbAccess.Repository.IRepository;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ApresentaçãoMJV.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClienteAPIController : ControllerBase
    {
        private readonly IClienteRepository _clienteRepository;

        public ClienteAPIController(IClienteRepository clienteRepository)
        {
            _clienteRepository = clienteRepository;
        }

        // GET: api/<clienteAPIController>
        [HttpGet]
        public ActionResult<IEnumerable<Cliente>> GetAll()
        {
            IEnumerable<Cliente> clientes = new List<Cliente>();
            try
            {
                clientes = _clienteRepository.GetAll().ToList();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Erro na API GET clientes");
                Console.WriteLine(ex.Message);
            }
            return Ok(clientes);
        }

        // GET api/<clienteAPIController>/5
        [HttpGet("{id}")]
        public ActionResult<Cliente> Get(int id)
        {
            Cliente cliente = new Cliente();
            try
            {
                cliente = _clienteRepository.Get(id);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Erro na API GET Cliente");
                Console.WriteLine(ex.Message);
            }
            return Ok(cliente);
        }

        // POST api/<clienteAPIController>
        [HttpPost]
        public void Post([FromBody] Cliente cliente)
        {
            try
            {
                _clienteRepository.Add(cliente);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Erro na API Add Cliente");
                Console.WriteLine(ex.Message);
            }
        }

        // PUT api/<clienteAPIController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] Cliente cliente)
        {
            try
            {
                _clienteRepository.Update(cliente);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Erro na API Update Cliente");
                Console.WriteLine(ex.Message);
            }
        }

        // DELETE api/<clienteAPIController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            try
            {
                _clienteRepository.Delete(id);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Erro na API Delete Cliente");
                Console.WriteLine(ex.Message);
            }
        }
    }
}
