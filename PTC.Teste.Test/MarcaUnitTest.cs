using Microsoft.VisualStudio.TestTools.UnitTesting;
using PTC.Teste.Context;
using PTC.Teste.Entity;
using PTC.Teste.Entity.Enum;
using PTC.Teste.Repository;

namespace PTC.Teste.TestUnit
{
    [TestClass]
    public class MarcaUnitTest
    {
        [TestMethod]
        public void IncluirTest()
        {
            string nome = "Fiat";
            Situacao situacao = Situacao.Ativo;

            using PTCTesteContext context = new();

            using MarcaRepository marcaRepository = new(context);

            marcaRepository.Incluir(new Marca()
            {
                Nome = nome,
                Situacao = situacao,
            });
        }
    }
}
