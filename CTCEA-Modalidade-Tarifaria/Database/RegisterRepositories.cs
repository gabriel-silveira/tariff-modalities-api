using CTCEA_Tariff_Modalities.Database.Repositories;
using CTCEA_Tariff_Modalities.Database.Repositories.Base;
using CTCEA_Tariff_Modalities.Database.Repositories.Interfaces;

namespace CTCEA_Tariff_Modalities.Database;

public static class RegisterRepositories
{
    public static void Register(IServiceCollection services)
    {
        //Repositories base
        services.AddScoped(typeof(IRepositoryBase<>), typeof(RepositoryBase<>));

        services.AddTransient<IImmediateBillingRepository, CobrancaAVistaRepository>();
    }
}
