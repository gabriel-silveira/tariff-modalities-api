using CTCEA_Modalidade_Tarifaria.Entities;
using CTCEA_Modalidade_Tarifaria.Database.Context;
using CTCEA_Modalidade_Tarifaria.Repositories.Base;
using CTCEA_Modalidade_Tarifaria.Database.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using CTCEA_Modalidade_Tarifaria.Models.CobrancaAVista.DTO;

namespace CTCEA_Modalidade_Tarifaria.Database.Repositories
{
    public class CobrancaAVistaRepository : RepositoryBase<CobrancaAVista>, ICobrancaAVistaRepository
    {
        private readonly ModalidadeTarifariaContext _context;

        public CobrancaAVistaRepository(ModalidadeTarifariaContext context) : base(context)
        {
            _context = context;
        }

        public EmpresaVigenteResponseDTO EmpresaVigente(string ciaIcao, string IcaoLocalidade, DateTime DataDecolagem)
        {
            var response = new EmpresaVigenteResponseDTO() {
                Cadastrada = false,
                Nacionalidade = "",
            };

            var query = _context.CobrancaAVista.AsQueryable();

            var result = query
                .Where(
                    cv => cv.CiaIcao == ciaIcao
                    && cv.DataInclusao <= DataDecolagem
                    && (
                        cv.DataSaida > DataDecolagem || cv.DataSaida == null
                    )
                    && (
                        cv.IcaoLocalidade == IcaoLocalidade || cv.IcaoLocalidade == null
                    )
                 )
                .OrderByDescending(cv => cv.DataInclusao)
                .FirstOrDefault();

            if (result != null)
            {
                response.Cadastrada = true;
                response.Nacionalidade = result.Nacionalidade;
            }

            return response;
        }

        public override IQueryable<CobrancaAVista> Includes(IQueryable<CobrancaAVista> query)
        {
            return query;
        }
    }
}
