using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using CTCEA_Tariff_Modalities.Entities;
using CTCEA_Tariff_Modalities.Entities.Mappings;

namespace CTCEA_Tariff_Modalities.Database.Context;

public class TariffModalitiesContext : IdentityDbContext
{
    public DbSet<ImmediateBilling> ImmediateBilling { get; set; }
    public DbSet<FlightCompany> FlightCompany { get; set; }

    public TariffModalitiesContext(DbContextOptions<TariffModalitiesContext> options) : base(options) { }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        builder.ApplyConfiguration(new ImmediateBillingMap());
        builder.ApplyConfiguration(new FlightCompanyMap());
    }
}
