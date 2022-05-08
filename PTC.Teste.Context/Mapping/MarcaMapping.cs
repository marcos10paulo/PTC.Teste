using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PTC.Teste.Context.Mapping.DefaultConfiguration;
using PTC.Teste.Entity;

namespace PTC.Teste.Context.Mapping
{
    public class MarcaMapping : DefaultConfigurationMapping<Marca>, IEntityTypeConfiguration<Marca>
    {
        public void Configure(EntityTypeBuilder<Marca> builder)
        {
            Configure(builder, "marca");

            builder.Property(c => c.Nome).HasColumnName("nome").HasMaxLength(100).IsRequired();
            builder.Property(c => c.Situacao).HasColumnName("status").IsRequired();

            builder.HasMany(c => c.Veiculo).WithOne(c => c.Marca);
        }
    }
}
