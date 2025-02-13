using CTCEA_Tariff_Modalities.Database.Repositories.Interfaces;
using CTCEA_Tariff_Modalities.Entities;
using CTCEA_Tariff_Modalities.Models.ImmediateBilling.DTO;
using CTCEA_Tariff_Modalities.Services.Interfaces;

namespace CTCEA_Tariff_Modalities.Services
{
    public class ImmediateBillingService : ServiceBase<ImmediateBilling>, IImmediateBillingService
    {
        private readonly IImmediateBillingRepository _immediateBillingRepository;

        public ImmediateBillingService(IImmediateBillingRepository repository) : base(repository)
        {
            _immediateBillingRepository = repository;
        }

        public CompanyInForceResponseDTO CompanyInForce(string icaoAirportCode, string IcaoLocale, DateTime DepartureDate)
        {
            var result = _immediateBillingRepository.CompanyInForce(icaoAirportCode, IcaoLocale, DepartureDate);

            return result;
        }
    }
}
