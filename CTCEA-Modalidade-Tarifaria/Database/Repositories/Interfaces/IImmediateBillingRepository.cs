using CTCEA_Tariff_Modalities.Entities;
using CTCEA_Tariff_Modalities.Database.Repositories.Base;
using CTCEA_Tariff_Modalities.Models.ImmediateBilling.DTO;

namespace CTCEA_Tariff_Modalities.Database.Repositories.Interfaces;

public interface IImmediateBillingRepository : IRepositoryBase<ImmediateBilling>
{
    public CompanyInForceResponseDTO CompanyInForce(string icaoAirportCode, string IcaoLocale, DateTime DepartureDate);
}
