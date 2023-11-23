using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace DbAccess.Models
{
    public class Locacao
    {
        [Required]
        [JsonPropertyName("id")]
        public int Id { get; set; }
        [Required]
        [JsonPropertyName("clienteid")]
        [DisplayName("Cliente")]
        public int ClienteId { get; set; }
        [Required]
        [JsonPropertyName("carroid")]
        [DisplayName("Carro")]
        public int CarroId { get; set; }
        [Required]
        [JsonPropertyName("datalocacao")]
        [DisplayName("Data de Locação")] // "Data de Locação
        public DateTime DataLocacao { get; set; }
        [JsonPropertyName("datadevolucao")]
        [DisplayName("Data de Devolução")] // "Data de Devolução
        public DateTime DataDevolucao { get; set; }
        [JsonPropertyName("valortotal")]
        [DisplayName("Valor Total")]
        public decimal ValorTotal { get; set; }
    }
}
