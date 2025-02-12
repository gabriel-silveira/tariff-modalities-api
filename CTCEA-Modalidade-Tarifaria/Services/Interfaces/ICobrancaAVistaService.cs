using CTCEA_Modalidade_Tarifaria.Models.CobrancaAVista.DTO;

namespace CTCEA_Modalidade_Tarifaria.Services.Interfaces
{
    public interface ICobrancaAVistaService
    {
        public EmpresaVigenteResponseDTO EmpresaVigente(string icao, string IcaoLocalidade, DateTime DataDecolagem);
    }
}
