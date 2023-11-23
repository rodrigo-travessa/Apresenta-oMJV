using DbAccess.Models;

namespace FrontEnd.Models
{
    public class CompleteDataViewModel
    {
        public List<Carro> Carros { get; set; } = new List<Carro>();
        public List<Locacao> Locacoes { get; set; } = new List<Locacao>();
        public List<Cliente> Clientes { get; set; } = new List<Cliente>();
    }
}
