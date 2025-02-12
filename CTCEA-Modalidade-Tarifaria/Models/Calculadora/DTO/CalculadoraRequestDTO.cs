using System.Text.Json.Serialization;

namespace CTCEA_Modalidade_Tarifaria.Models.Calculadora.DTO
{
    public class CalculadoraRequestDTO
    {
        [JsonPropertyName("grupo")]
        public required string Grupo { get; set; }

        [JsonPropertyName("natureza")]
        public required string Natureza { get; set; }

        [JsonPropertyName("origem")]
        public required string Origem { get; set; }

        [JsonPropertyName("destino")]
        public required string Destino { get; set; }

        [JsonPropertyName("pmd")]
        public required int PMD { get; set; }
    }
}
