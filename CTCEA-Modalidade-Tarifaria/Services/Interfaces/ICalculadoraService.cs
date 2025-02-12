using CTCEA_Modalidade_Tarifaria.Models.Base.DTO;
using CTCEA_Modalidade_Tarifaria.Models.Calculadora.DTO;

namespace CTCEA_Modalidade_Tarifaria.Services.Interfaces
{
    public interface ICalculadoraService
    {
        public Task<ResponseBaseDTO<CalculadoraResponseDTO>> CalcularTarifas(CalculadoraRequestDTO request);
    }
}
