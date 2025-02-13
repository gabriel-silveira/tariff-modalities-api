using CTCEA_Tariff_Modalities.Models.Base.DTO;
using CTCEA_Tariff_Modalities.Models.Classificacao.DTO;

namespace CTCEA_Tariff_Modalities.Services.Interfaces
{
    public interface IClassificationService
    {
        Task<ResponseBaseDTO<ClassificationResponseDTO>> ClassifyFlight(ClassificationRequestDTO request);

        public string GetICAOAirpotCode();

        public bool ValidCallsign();

        public bool ForeignRegistration(string siglaIcao);
    }
}
