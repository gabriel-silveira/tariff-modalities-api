using System.Text.Json.Serialization;

namespace CTCEA_Modalidade_Tarifaria.Models.CobrancaAVista.DTO
{
    public class EmpresaVigenteResponseDTO
    {
        [JsonPropertyName("cadastrada")]
        public required bool Cadastrada { get; set; }

        [JsonPropertyName("nacionalidade")]
        public required string Nacionalidade { get; set; }
    }
}
