using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.Extensions.Configuration;
using PTC.Teste.Context.Mapping;
using PTC.Teste.Entity;
using System.Text;

namespace PTC.Teste.Context
{
    public class PTCTesteContext: DbContext
    {
        public PTCTesteContext()
        {
            this.ChangeTracker.LazyLoadingEnabled = false;

            if (!Database.GetService<IRelationalDatabaseCreator>().Exists())
            {
                Database.EnsureCreated();
                PTCTesteInitialize.Seed(this);
            }
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            var configuration = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build();

            var provider = configuration.GetSection("AppSettings").GetValue<string>("DatabaseProvider");
            if (provider.Equals("SqlServer"))
                optionsBuilder.UseSqlServer(configuration.GetConnectionString("PTC.Teste.ConnectionString"));
            
            Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new AutenticacaoMapping());
            modelBuilder.ApplyConfiguration(new MarcaMapping());
            modelBuilder.ApplyConfiguration(new ProprietarioMapping());
            modelBuilder.ApplyConfiguration(new VeiculoMapping());

            base.OnModelCreating(modelBuilder);
        }

        public DbSet<Autenticacao> Autenticacoes { get; set; }
        public DbSet<Marca> Marcas { get; set; }
        public DbSet<Proprietario> Proprietarios { get; set; }
        public DbSet<Veiculo> Veiculos { get; set; }
    }
}
