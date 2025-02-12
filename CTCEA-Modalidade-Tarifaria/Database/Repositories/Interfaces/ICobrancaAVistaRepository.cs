using CTCEA_Modalidade_Tarifaria.Entities;
using CTCEA_Modalidade_Tarifaria.Database.Repositories.Base;
using CTCEA_Modalidade_Tarifaria.Models.CobrancaAVista.DTO;

namespace CTCEA_Modalidade_Tarifaria.Database.Repositories.Interfaces;

public interface ICobrancaAVistaRepository : IRepositoryBase<CobrancaAVista>
{
    public EmpresaVigenteResponseDTO EmpresaVigente(string ciaIcao, string IcaoLocalidade, DateTime DataDecolagem);
}
