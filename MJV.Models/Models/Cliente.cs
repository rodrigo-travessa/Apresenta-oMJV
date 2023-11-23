using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace DbAccess.Models
{
    public class Cliente
    {
        [Required]
        [JsonPropertyName("id")]
        public int Id { get; set; }
        [Required]
        [JsonPropertyName("nome")]
        public string Nome { get; set; }
        [Required]
        [JsonPropertyName("sobrenome")]
        public string Sobrenome { get; set; }
    }
}
