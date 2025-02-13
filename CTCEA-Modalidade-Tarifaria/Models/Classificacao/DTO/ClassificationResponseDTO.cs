using System.Text.Json.Serialization;

namespace CTCEA_Tariff_Modalities.Models.Classificacao.DTO
{
    public class ClassificationResponseDTO
    {
        [JsonPropertyName("grupo")]
        public required string Grupo { get; set; }

        [JsonPropertyName("tipo_cobranca")]
        public required string TipoCobranca { get; set; }

        [JsonPropertyName("natureza")]
        public required string Natureza { get; set; }
    }
}
