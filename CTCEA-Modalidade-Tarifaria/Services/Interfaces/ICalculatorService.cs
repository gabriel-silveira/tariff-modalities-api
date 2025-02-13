using CTCEA_Tariff_Modalities.Models.Base.DTO;
using CTCEA_Tariff_Modalities.Models.Calculator.DTO;

namespace CTCEA_Tariff_Modalities.Services.Interfaces
{
    public interface ICalculatorService
    {
        public Task<ResponseBaseDTO<CalculatorResponseDTO>> CalcularTarifas(CalculatorRequestDTO request);
    }
}
