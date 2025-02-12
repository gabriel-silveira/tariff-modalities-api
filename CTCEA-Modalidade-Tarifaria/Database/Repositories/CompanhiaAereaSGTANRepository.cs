using CTCEA_Modalidade_Tarifaria.Entities;
using CTCEA_Modalidade_Tarifaria.Database.Context;
using CTCEA_Modalidade_Tarifaria.Repositories.Base;
using CTCEA_Modalidade_Tarifaria.Database.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace CTCEA_Modalidade_Tarifaria.Database.Repositories
{
    public class CompanhiaAereaSGTANRepository : RepositoryBase<CompanhiaAerea>, ICompanhiaAereaSGTANRepository
    {
        private readonly ModalidadeTarifariaContext _context;

        public CompanhiaAereaSGTANRepository(ModalidadeTarifariaContext context) : base(context)
        {
            _context = context;
        }

        public async Task<bool> ObterCompanhia(string ciaIcao)
        {
            var query = _context.CompanhiaAerea.AsQueryable();

            var result = await query
                .Where(cv => cv.CiaIcao == ciaIcao)
                .ToListAsync();

            return result.Count() > 0;
        }

        public override IQueryable<CompanhiaAerea> Includes(IQueryable<CompanhiaAerea> query)
        {
            return query;
        }
    }
}
