using System.Text.Json.Serialization;

namespace CTCEA_Tariff_Modalities.Models.Calculator.DTO
{
    public class CalculatorResponseDTO
    {
        [JsonPropertyName("cotacao_dolar")]
        public required string CotacaoDolar { get; set; }

        [JsonPropertyName("data_cotacao_dolar")]
        public required string DataCotacaoDolar { get; set; }

        [JsonPropertyName("tan")]
        public required string TAN { get; set; }

        [JsonPropertyName("tatApp")]
        public required string TATAPP { get; set; }

        [JsonPropertyName("tatAdr")]
        public required string TATADR { get; set; }

        [JsonPropertyName("total")]
        public required string TOTAL { get; set; }

        [JsonPropertyName("tan_dolar")]
        public required string TAN_DOLAR { get; set; }

        [JsonPropertyName("tatApp_dolar")]
        public required string TATAPP_DOLAR { get; set; }

        [JsonPropertyName("tatAdr_dolar")]
        public required string TATADR_DOLAR { get; set; }

        [JsonPropertyName("total_dolar")]
        public required string TOTAL_DOLAR { get; set; }

        [JsonPropertyName("error")]
        public required string ERROR { get; set; }
    }
}
