using CTCEA_Modalidade_Tarifaria.Database.Repositories.Interfaces;
using CTCEA_Modalidade_Tarifaria.Entities;
using CTCEA_Modalidade_Tarifaria.Models.CobrancaAVista.DTO;
using CTCEA_Modalidade_Tarifaria.Services.Interfaces;

namespace CTCEA_Modalidade_Tarifaria.Services
{
    public class CobrancaAVistaService : ServiceBase<CobrancaAVista>, ICobrancaAVistaService
    {
        private readonly ICobrancaAVistaRepository _cobrancaAVistaRepository;

        public CobrancaAVistaService(ICobrancaAVistaRepository repository) : base(repository)
        {
            _cobrancaAVistaRepository = repository;
        }

        public EmpresaVigenteResponseDTO EmpresaVigente(string ciaoIcao, string IcaoLocalidade, DateTime DataDecolagem)
        {
            var result = _cobrancaAVistaRepository.EmpresaVigente(ciaoIcao, IcaoLocalidade, DataDecolagem);

            return result;
        }
    }
}
