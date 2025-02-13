using System.Text.Json.Serialization;

namespace CTCEA_Tariff_Modalities.Models.Base.DTO
{
    public class ErrorResponseDTO
    {
        [JsonPropertyName("message")]
        public required string Message { get; set; }

        public ErrorResponseDTO MissingField(string fieldName)
        {
            Message = "O campo " + fieldName + " é obrigatório.";

            return this;
        }
        public ErrorResponseDTO Set(string text)
        {
            Message = text;

            return this;
        }
    }
}
