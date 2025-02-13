using CTCEA_Tariff_Modalities.Entities;
using CTCEA_Tariff_Modalities.Database.Context;
using CTCEA_Tariff_Modalities.Database.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using CTCEA_Tariff_Modalities.Database.Repositories.Base;

namespace CTCEA_Tariff_Modalities.Database.Repositories
{
    public class CompanhiaAereaSGTANRepository : RepositoryBase<FlightCompany>, ICompanhiaAereaSGTANRepository
    {
        private new readonly TariffModalitiesContext _context;

        public CompanhiaAereaSGTANRepository(TariffModalitiesContext context) : base(context)
        {
            _context = context;
        }

        public async Task<bool> ObterCompanhia(string icaoAirpotCode)
        {
            var query = _context.FlightCompany.AsQueryable();

            var result = await query
                .Where(cv => cv.CiaIcao == icaoAirpotCode)
                .ToListAsync();

            return result.Count() > 0;
        }

        public override IQueryable<FlightCompany> Includes(IQueryable<FlightCompany> query)
        {
            return query;
        }
    }
}
