using CTCEA_Modalidade_Tarifaria.Database.Repositories.Interfaces;
using CTCEA_Modalidade_Tarifaria.Entities;
using CTCEA_Modalidade_Tarifaria.Services.Interfaces;

namespace CTCEA_Modalidade_Tarifaria.Services
{
    public class CompanhiaAereaSGTANService : ServiceBase<CompanhiaAerea>, ICompanhiaAereaSGTANService
    {
        private readonly ICompanhiaAereaSGTANRepository _companhiaAereaSGTANRepository;

        public CompanhiaAereaSGTANService(ICompanhiaAereaSGTANRepository repository) : base(repository)
        {
            _companhiaAereaSGTANRepository = repository;
        }

        public async Task<bool> ObterCompanhia(string ciaoIcao)
        {
            var result = await _companhiaAereaSGTANRepository.ObterCompanhia(ciaoIcao);

            return result;
        }
    }
}
