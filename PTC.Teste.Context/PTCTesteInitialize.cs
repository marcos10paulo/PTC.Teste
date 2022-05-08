using PTC.Teste.Entity;
using System.Linq;

namespace PTC.Teste.Context
{
    public class PTCTesteInitialize
    {
        public static void Seed(PTCTesteContext context)
        {
            if (context.Autenticacoes.Local.Count == 0)
            {
                context.Autenticacoes.Add(new Autenticacao { Login = "796BA633-DE84-4738-AA35-D427BD2C9F50", Senha = "E6804D04057A5152912C895943119F6D" });               

                context.Marcas.Add(new Marca { Nome = "Renaut", Situacao = Entity.Enum.Situacao.Ativo });

                context.Proprietarios.Add(new Proprietario { Nome = "Marcos Paulo Silva", Documento = "12345678909", Email = "marcos10paulo@gmail.com", Cep = "35500001", Endereco = "Rua a", Numero = "100", Bairro = "Centro", Cidade = "Divinopolis", Estado = "MG", Status = Entity.Enum.Situacao.Ativo });

                context.SaveChanges();
            }
        }
    }
}
