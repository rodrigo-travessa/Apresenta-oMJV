using DbAccess.Models;

namespace FrontEnd.Models
{
    public class LocacaoCarroClienteViewModel
    {
        public Locacao Locacao { get; set; } = new Locacao();
        public Carro Carro { get; set; } = new Carro();
        public Cliente Cliente { get; set; } = new Cliente();
    }
}
