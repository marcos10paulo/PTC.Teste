using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PTC.Teste.Context.Mapping.DefaultConfiguration;
using PTC.Teste.Entity;

namespace PTC.Teste.Context.Mapping
{
    public class AutenticacaoMapping : DefaultConfigurationMapping<Autenticacao>, IEntityTypeConfiguration<Autenticacao>
    {
        public void Configure(EntityTypeBuilder<Autenticacao> builder)
        {
            Configure(builder, "Autenticacao");

            builder.Property(p => p.Login).HasMaxLength(80).IsRequired();
            builder.Property(p => p.Senha).HasMaxLength(80).IsRequired();
        }
    }
}
