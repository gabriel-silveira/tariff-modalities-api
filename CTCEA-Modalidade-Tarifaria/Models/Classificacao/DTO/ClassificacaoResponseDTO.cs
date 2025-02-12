using System.Text.Json.Serialization;

namespace CTCEA_Modalidade_Tarifaria.Models.Classificacao.DTO
{
    public class ClassificacaoResponseDTO
    {
        [JsonPropertyName("grupo")]
        public required string Grupo { get; set; }

        [JsonPropertyName("tipo_cobranca")]
        public required string TipoCobranca { get; set; }

        [JsonPropertyName("natureza")]
        public required string Natureza { get; set; }
    }
}
