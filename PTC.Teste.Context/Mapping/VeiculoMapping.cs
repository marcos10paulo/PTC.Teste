using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PTC.Teste.Context.Mapping.DefaultConfiguration;
using PTC.Teste.Entity;

namespace PTC.Teste.Context.Mapping
{
    public class VeiculoMapping : DefaultConfigurationMapping<Veiculo>, IEntityTypeConfiguration<Veiculo>
    {
        public void Configure(EntityTypeBuilder<Veiculo> builder)
        {
            Configure(builder, "veiculo");

            builder.Property(c => c.ProprietarioId).HasColumnName("proprietario_id").IsRequired();
            builder.Property(c => c.Renavam).HasColumnName("renavem").HasMaxLength(20).IsRequired();
            builder.Property(c => c.MarcaId).HasColumnName("marca_id").IsRequired();
            builder.Property(c => c.Modelo).HasColumnName("modelo").HasMaxLength(50).IsRequired();
            builder.Property(c => c.AnoFabricacao).HasColumnName("ano_fabricacao").IsRequired();
            builder.Property(c => c.AnoModelo).HasColumnName("ano_modelo").IsRequired();
            builder.Property(c => c.Quilometragem).HasColumnName("quilometragem").IsRequired();
            builder.Property(c => c.Valor).HasColumnName("valor").HasPrecision(12, 2).IsRequired();
            builder.Property(c => c.SituacaoVeiculo).HasColumnName("status").IsRequired();

        }
    }
}
