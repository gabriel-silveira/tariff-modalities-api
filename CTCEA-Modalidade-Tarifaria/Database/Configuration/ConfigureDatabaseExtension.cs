using Microsoft.EntityFrameworkCore;
using CTCEA_Modalidade_Tarifaria.Database.Context;

namespace CTCEA_Modalidade_Tarifaria.Database.Configuration;

public static class ConfigureDatabaseExtension
{
    public static void ConfigureDatabase(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<ModalidadeTarifariaContext>(
            opcoes => opcoes.UseOracle(configuration.GetConnectionString("DefaultConnection")
        ));
    }
}
