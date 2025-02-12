using System.Text.Json.Serialization;

namespace CTCEA_Modalidade_Tarifaria.Models.Classificacao.DTO
{
    public class ClassificacaoRequestDTO
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
