using CTCEA_Tariff_Modalities.Entities;
using CTCEA_Tariff_Modalities.Database.Repositories.Base;

namespace CTCEA_Tariff_Modalities.Database.Repositories.Interfaces;

public interface ICompanhiaAereaSGTANRepository : IRepositoryBase<FlightCompany>
{
    public Task<bool> ObterCompanhia(string ciaIcao);
}
