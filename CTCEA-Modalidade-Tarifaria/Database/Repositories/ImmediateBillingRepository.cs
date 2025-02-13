using CTCEA_Tariff_Modalities.Entities;
using CTCEA_Tariff_Modalities.Database.Context;
using CTCEA_Tariff_Modalities.Database.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using CTCEA_Tariff_Modalities.Models.ImmediateBilling.DTO;
using CTCEA_Tariff_Modalities.Database.Repositories.Base;

namespace CTCEA_Tariff_Modalities.Database.Repositories
{
    public class CobrancaAVistaRepository : RepositoryBase<ImmediateBilling>, IImmediateBillingRepository
    {
        private readonly TariffModalitiesContext _context;

        public CobrancaAVistaRepository(TariffModalitiesContext context) : base(context)
        {
            _context = context;
        }

        public CompanyInForceResponseDTO CompanyInForce(string icaoAirportCode, string IcaoLocale, DateTime DepartureDate)
        {
            var response = new CompanyInForceResponseDTO() {
                Cadastrada = false,
                Nacionalidade = "",
            };

            var query = _context.ImmediateBilling.AsQueryable();

            var result = query
                .Where(
                    cv => cv.CiaIcao == icaoAirportCode
                    && cv.DataInclusao <= DepartureDate
                    && (
                        cv.DataSaida > DepartureDate || cv.DataSaida == null
                    )
                    && (
                        cv.IcaoLocalidade == IcaoLocale || cv.IcaoLocalidade == null
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

        public override IQueryable<ImmediateBilling> Includes(IQueryable<ImmediateBilling> query)
        {
            return query;
        }
    }
}
