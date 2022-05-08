using PTC.Teste.Context;
using System;
using System.Linq;

namespace PTC.Teste.Repository
{
    public class AutenticacaoRepository : IDisposable
    {
        private readonly PTCTesteContext _db = new();

        public AutenticacaoRepository()
        {

        }
        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                _db.Dispose();
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        public bool Check(string login, string senha)
        {
            return (from q in _db.Autenticacoes where q.Login == login && q.Senha == senha select q).Any();
        }
    }
}
