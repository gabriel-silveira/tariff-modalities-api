using System.Text.Json.Serialization;

namespace CTCEA_Tariff_Modalities.Models.Calculator.DTO
{
    public class CalculatorRequestDTO
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
