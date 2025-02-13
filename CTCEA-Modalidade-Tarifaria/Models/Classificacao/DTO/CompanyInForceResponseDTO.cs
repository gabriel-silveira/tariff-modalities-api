using System.Text.Json.Serialization;

namespace CTCEA_Tariff_Modalities.Models.ImmediateBilling.DTO
{
    public class CompanyInForceResponseDTO
    {
        [JsonPropertyName("cadastrada")]
        public required bool Cadastrada { get; set; }

        [JsonPropertyName("nacionalidade")]
        public required string Nacionalidade { get; set; }
    }
}
