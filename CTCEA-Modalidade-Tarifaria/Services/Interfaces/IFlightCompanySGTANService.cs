namespace CTCEA_Tariff_Modalities.Services.Interfaces
{
    public interface IFlightCompanySGTANService
    {
        public Task<bool> GetCompany(string icao);
    }
}
