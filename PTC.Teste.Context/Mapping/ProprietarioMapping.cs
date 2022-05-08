    using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PTC.Teste.Context.Mapping.DefaultConfiguration;
using PTC.Teste.Entity;

namespace PTC.Teste.Context.Mapping
{
    public class ProprietarioMapping : DefaultConfigurationMapping<Proprietario>, IEntityTypeConfiguration<Proprietario>
    {
        public void Configure(EntityTypeBuilder<Proprietario> builder)
        {
            Configure(builder, "proprietario");

            builder.Property(c => c.Nome).HasColumnName("nome").HasMaxLength(100).IsRequired();
            builder.Property(c => c.Documento).HasColumnName("documento").HasMaxLength(11).IsRequired();
            builder.Property(c => c.Email).HasColumnName("email").HasMaxLength(100).IsRequired();
            builder.Property(c => c.Cep).HasColumnName("cep").HasMaxLength(20).IsRequired();
            builder.Property(c => c.Endereco).HasColumnName("endereco").HasMaxLength(100).IsRequired();
            builder.Property(c => c.Numero).HasColumnName("numero").HasMaxLength(20).IsRequired();
            builder.Property(c => c.Bairro).HasColumnName("bairro").HasMaxLength(50).IsRequired();
            builder.Property(c => c.Cidade).HasColumnName("cidade").HasMaxLength(50).IsRequired();
            builder.Property(c => c.Estado).HasColumnName("estado").HasMaxLength(2).IsRequired();
            builder.Property(c => c.Status).HasColumnName("status").IsRequired();

            builder.HasMany(c => c.Veiculo).WithOne(c => c.Proprietario);
        }
    }
}
