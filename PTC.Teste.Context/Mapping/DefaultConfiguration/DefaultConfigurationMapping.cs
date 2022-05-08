using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PTC.Teste.Entity;

namespace PTC.Teste.Context.Mapping.DefaultConfiguration
{
    public class DefaultConfigurationMapping<TEntity> where TEntity : EntityBase
    {
        public void Configure(EntityTypeBuilder<TEntity> builder, string tablename)
        {
            builder.ToTable(tablename.ToLower());

            builder.HasKey(c => c.Id);

            builder.Property(c => c.Id).HasColumnName("id").ValueGeneratedOnAdd().IsRequired();
        }
    }
}
