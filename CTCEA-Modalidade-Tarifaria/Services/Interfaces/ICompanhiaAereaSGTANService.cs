namespace CTCEA_Modalidade_Tarifaria.Services.Interfaces
{
    public interface ICompanhiaAereaSGTANService
    {
        public Task<bool> ObterCompanhia(string icao);
    }
}
