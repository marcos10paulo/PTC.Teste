using Microsoft.VisualStudio.TestTools.UnitTesting;
using PTC.Teste.Context;
using PTC.Teste.Entity;
using PTC.Teste.Entity.Enum;
using PTC.Teste.Repository;

namespace PTC.Teste.TestUnit
{
    [TestClass]
    public class VeiculoUnitTest
    {
        [TestMethod]
        public void IncluirTest()
        {
            int proprietarioId = 1;
            string renavam = "12345678901234567890";
            int marcaId = 1;
            string modelo = "Teste";
            int anoFabricacao = 2000;
            int anoModelo = 2000;
            int quilometragem = 100000;
            decimal valor = 20000;

            using var context = new PTCTesteContext();

            using VeiculoRepository veiculoRepository = new(context);

            veiculoRepository.Incluir(new Veiculo()
            {
                ProprietarioId = proprietarioId,
                Renavam = renavam,
                MarcaId = marcaId,
                Modelo = modelo,
                AnoFabricacao = anoFabricacao,
                AnoModelo = anoModelo,
                Quilometragem = quilometragem,
                Valor = valor,
                SituacaoVeiculo = SituacaoVeiculo.Disponivel
            });
        }
    }
}
