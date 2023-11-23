using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace DbAccess.Models
{
    public class Carro
    {
        [Required]
        [JsonPropertyName("id")]
        public int Id { get; set; }
        [Required]
        [JsonPropertyName("modelo")]
        public string Modelo { get; set; }
        [Required]
        [JsonPropertyName("marca")]
        public string Marca { get; set; }
        [Required]
        [JsonPropertyName("precodiaria")]
        [DisplayName("Preço Diária")]
        public decimal PrecoDiaria { get; set; }
        [Required]
        [JsonPropertyName("precokm")]
        [DisplayName("Preço KM")]
        public decimal PrecoKM { get; set; }
        [JsonPropertyName("estado")]
        [DisplayName("Disponibilidade")]
        public int Estado { get; set; } = 0; // 0 = Disponível, 1 = Alugado
    }
}
