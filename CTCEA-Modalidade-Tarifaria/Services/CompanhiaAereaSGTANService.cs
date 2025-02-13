using CTCEA_Tariff_Modalities.Database.Repositories.Interfaces;
using CTCEA_Tariff_Modalities.Entities;
using CTCEA_Tariff_Modalities.Services.Interfaces;

namespace CTCEA_Tariff_Modalities.Services
{
    public class CompanhiaAereaSGTANService : ServiceBase<FlightCompany>, IFlightCompanySGTANService
    {
        private readonly ICompanhiaAereaSGTANRepository _companhiaAereaSGTANRepository;

        public CompanhiaAereaSGTANService(ICompanhiaAereaSGTANRepository repository) : base(repository)
        {
            _companhiaAereaSGTANRepository = repository;
        }

        public async Task<bool> GetCompany(string ciaoIcao)
        {
            var result = await _companhiaAereaSGTANRepository.ObterCompanhia(ciaoIcao);

            return result;
        }
    }
}
