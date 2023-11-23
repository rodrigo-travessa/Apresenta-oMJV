using DbAccess.Models;
using DbAccess.Repository.IRepository;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ApresentaçãoMJV.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CarroAPIController : ControllerBase
    {
        private readonly ICarroRepository _carroRepository;

        public CarroAPIController(ICarroRepository carroRepository)
        {
            _carroRepository = carroRepository;
        }

        // GET: api/<CarroAPIController>
        [HttpGet]
        public ActionResult<IEnumerable<Carro>> GetAll()
        {
            IEnumerable<Carro> carros = new List<Carro>();
            try
            {
                carros = _carroRepository.GetAll().ToList();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Erro na API GET Carros");
                Console.WriteLine(ex.Message);
            }
            return Ok(carros);
        }

        // GET api/<CarroAPIController>/5
        [HttpGet("{id}")]
        public ActionResult<Carro> Get(int id)
        {
            Carro carro = new Carro();
            try
            {
                carro = _carroRepository.Get(id);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Erro na API GET Carro");
                Console.WriteLine(ex.Message);
            }
            return Ok(carro);
        }

        // POST api/<CarroAPIController>
        [HttpPost]
        public void Post([FromBody] Carro carro)
        {
            try
            {
                _carroRepository.Add(carro);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Erro na API Add Carro");
                Console.WriteLine(ex.Message);
            }
        }

        // PUT api/<CarroAPIController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] Carro carro)
        {
            try
            {
                _carroRepository.Update(carro);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Erro na API Update Carro");
                Console.WriteLine(ex.Message);
            }
        }
        [HttpPut]
        [Route("UpdateDisponibilidade/{id}")]
        public void UpdateDisponibilidade(int id)
        {
            try
            {
                Carro carro = _carroRepository.Get(id);
                if (carro.Estado == 0)
                {
                    carro.Estado = 1;
                }
                else
                {
                    carro.Estado = 0;
                }
                _carroRepository.Update(carro);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Erro na API Update Carro");
                Console.WriteLine(ex.Message);
            }
        }


        // DELETE api/<CarroAPIController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            try
            {
                _carroRepository.Delete(id);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Erro na API Delete Carro");
                Console.WriteLine(ex.Message);
            }
        }
    }
}
