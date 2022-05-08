using Microsoft.VisualStudio.TestTools.UnitTesting;
using PTC.Teste.Context;
using PTC.Teste.Entity;
using PTC.Teste.Entity.Enum;
using PTC.Teste.Repository;

namespace PTC.Teste.TestUnit
{
    [TestClass]
    public class ProprietarioUnitTest
    {
        [TestMethod]
        public void IncluirTest()
        {
            string nome = "Fiat";
            string documento = "12345678909";
            string email = "email@email.com";
            string cep = "35500000";
            string endereco = "Rua a";
            string numero = "100";
            string bairro = "Centro";
            string cidade = "Divinopolis";
            string estado = "MG";

            using PTCTesteContext context = new ();

            using ProprietarioRepository proprietarioRepository = new(context);

            proprietarioRepository.Incluir(new Proprietario()
            {
                Nome = nome,
                Documento = documento,
                Email = email,
                Cep = cep,
                Endereco = endereco,
                Numero = numero,
                Bairro = bairro,
                Cidade = cidade,
                Estado = estado,
                Status = Situacao.Ativo
            });
        }
    }
}
