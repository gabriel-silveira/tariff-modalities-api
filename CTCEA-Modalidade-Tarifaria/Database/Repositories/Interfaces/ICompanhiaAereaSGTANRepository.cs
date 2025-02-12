using CTCEA_Modalidade_Tarifaria.Entities;
using CTCEA_Modalidade_Tarifaria.Database.Repositories.Base;

namespace CTCEA_Modalidade_Tarifaria.Database.Repositories.Interfaces;

public interface ICompanhiaAereaSGTANRepository : IRepositoryBase<CompanhiaAerea>
{
    public Task<bool> ObterCompanhia(string ciaIcao);
}
