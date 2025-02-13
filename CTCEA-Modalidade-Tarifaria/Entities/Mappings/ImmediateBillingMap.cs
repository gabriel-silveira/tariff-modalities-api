using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CTCEA_Tariff_Modalities.Entities.Mappings;

public class ImmediateBillingMap : IEntityTypeConfiguration<ImmediateBilling>
{
    public void Configure(EntityTypeBuilder<ImmediateBilling> builder)
    {
        builder.HasKey(t => t.Id);

        builder.Property(t => t.Id)
               .HasColumnName("ID_COBRANCA_VISTA")
               .IsRequired()
               .UseIdentityColumn();

        builder.Property(t => t.MatriculaAeronave)
               .HasColumnName("VL_MATRICULA_AERONAVE")
               .IsRequired()
               .HasMaxLength(10);

        builder.Property(t => t.CiaIcao)
               .HasColumnName("CD_CIA_ICAO")
               .IsRequired()
               .HasMaxLength(3);

        builder.Property(t => t.Nacionalidade)
               .HasColumnName("CD_NACIONALIDADE")
               .IsRequired()
               .HasMaxLength(1);

        builder.Property(t => t.IcaoLocalidade)
               .HasColumnName("CD_ICAO")
               .IsRequired(false)
               .HasMaxLength(4);

        builder.Property(t => t.DataInclusao)
               .HasColumnName("DT_INCLUSAO")
               .IsRequired();

        builder.Property(t => t.DataSaida)
               .HasColumnName("DT_SAIDA")
               .IsRequired(false);

        builder.ToTable("CAD_COBRANCA_VISTA");
    }
}
