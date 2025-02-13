using Microsoft.EntityFrameworkCore;
using CTCEA_Tariff_Modalities.Database.Context;

namespace CTCEA_Tariff_Modalities.Database.Configuration;

public static class ConfigureDatabaseExtension
{
    public static void ConfigureDatabase(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<TariffModalitiesContext>(
            opcoes => opcoes.UseOracle(configuration.GetConnectionString("DefaultConnection")
        ));
    }
}
