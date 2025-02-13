using CTCEA_Tariff_Modalities.Models.ImmediateBilling.DTO;

namespace CTCEA_Tariff_Modalities.Services.Interfaces
{
    public interface IImmediateBillingService
    {
        public CompanyInForceResponseDTO CompanyInForce(string icao, string IcaoLocalidade, DateTime DataDecolagem);
    }
}
