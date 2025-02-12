using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CTCEA_Modalidade_Tarifaria.Entities.Mappings;

public class CompanhiaAereaMap : IEntityTypeConfiguration<CompanhiaAerea>
{
    public void Configure(EntityTypeBuilder<CompanhiaAerea> builder)
    {
        builder.HasKey(t => t.Id);

        builder.Property(t => t.Id)
               .HasColumnName("ID_CIA_AEREA")
               .IsRequired()
               .UseIdentityColumn();

        builder.Property(t => t.CiaIcao)
               .HasColumnName("CD_CIA_ICAO")
               .IsRequired()
               .HasMaxLength(3);

        builder.ToTable("CAD_CIA_AEREA");
    }
}
