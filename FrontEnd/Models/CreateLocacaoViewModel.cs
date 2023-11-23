using DbAccess.Models;

namespace FrontEnd.Models
{
    public class CreateLocacaoViewModel
    {
        public Locacao Locacao { get; set; } = new Locacao();
        public List<Carro> Carros { get; set; } = new List<Carro>();
        public List<Cliente> Clientes { get; set; } = new List<Cliente>();
    }
}
