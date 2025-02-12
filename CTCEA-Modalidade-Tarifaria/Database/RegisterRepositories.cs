using CTCEA_Modalidade_Tarifaria.Database.Repositories;
using CTCEA_Modalidade_Tarifaria.Database.Repositories.Base;
using CTCEA_Modalidade_Tarifaria.Database.Repositories.Interfaces;
using CTCEA_Modalidade_Tarifaria.Repositories.Base;

namespace CTCEA_Modalidade_Tarifaria.Database;

public static class RegisterRepositories
{
    public static void Register(IServiceCollection services)
    {
        //Repositories base
        services.AddScoped(typeof(IRepositoryBase<>), typeof(RepositoryBase<>));

        services.AddTransient<ICobrancaAVistaRepository, CobrancaAVistaRepository>();
    }
}
