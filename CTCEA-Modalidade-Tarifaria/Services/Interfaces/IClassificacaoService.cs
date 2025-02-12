using CTCEA_Modalidade_Tarifaria.Models.Base.DTO;
using CTCEA_Modalidade_Tarifaria.Models.Classificacao.DTO;

namespace CTCEA_Modalidade_Tarifaria.Services.Interfaces
{
    public interface IClassificacaoService
    {

        Task<ResponseBaseDTO<ClassificacaoResponseDTO>> ClassificarVoo(ClassificacaoRequestDTO request);

        public string GetSiglaICAO();

        public bool CallsignValido();

        public bool MatriculaEstrangeira(string siglaIcao);
    }
}
