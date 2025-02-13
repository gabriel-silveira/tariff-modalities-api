using System.Text.Json.Serialization;

namespace CTCEA_Tariff_Modalities.Models.Classificacao.DTO
{
    public class ClassificationRequestDTO
    {
        [JsonPropertyName("icao_localidade")]
        public required string IcaoLocalidade { get; set; }

        [JsonPropertyName("identificacao")]
        public required string Identificacao { get; set; }

        [JsonPropertyName("origem")]
        public required string Origem { get; set; }

        [JsonPropertyName("destino")]
        public required string Destino { get; set; }

        [JsonPropertyName("data_decolagem")]
        public required DateTime DataDecolagem { get; set; }
    }
}
