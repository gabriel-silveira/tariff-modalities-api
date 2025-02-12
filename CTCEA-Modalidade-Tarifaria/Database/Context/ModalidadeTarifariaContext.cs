using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using CTCEA_Modalidade_Tarifaria.Entities;
using CTCEA_Modalidade_Tarifaria.Entities.Mappings;

namespace CTCEA_Modalidade_Tarifaria.Database.Context;

public class ModalidadeTarifariaContext : IdentityDbContext
{
    public DbSet<CobrancaAVista> CobrancaAVista { get; set; }
    public DbSet<CompanhiaAerea> CompanhiaAerea { get; set; }

    public ModalidadeTarifariaContext(DbContextOptions<ModalidadeTarifariaContext> opcoes) : base(opcoes) { }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        builder.ApplyConfiguration(new CobrancaAVistaMap());
        builder.ApplyConfiguration(new CompanhiaAereaMap());
    }
}
