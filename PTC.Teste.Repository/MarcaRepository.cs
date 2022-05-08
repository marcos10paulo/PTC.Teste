using PTC.Teste.Context;
using PTC.Teste.Entity;
using PTC.Teste.Entity.Enum;
using PTC.Teste.Repository.Repository;
using System;
using System.Linq;

namespace PTC.Teste.Repository
{
    public class MarcaRepository : IDefaultRepository<Marca>, IDisposable
    {
        private readonly PTCTesteContext _db = new();
        private readonly IRepository<Marca> _repository;

        public MarcaRepository(PTCTesteContext context = null)
        {
            _repository = new Repository<Marca>(context ?? new PTCTesteContext());
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

        public string Alterar(Marca entity)
        {
            string mensagem = ValidarDados(entity);
            if (mensagem == "")
            {
                mensagem = _repository.Update(entity);
            }
            return mensagem;
        }

        public string Excluir(int id)
        {
            string mensagem = ValidaExclusao();
            if (mensagem == "")
            {
                mensagem = _repository.Delete(id);
            }
            return mensagem;
        }

        public IQueryable<Marca> Filtrar(string condicao)
        {
            return _repository.Filter(condicao);
        }

        public string Incluir(Marca entity)
        {
            string mensagem = ValidarDados(entity);

            if (mensagem == "")
            {
                mensagem = _repository.Insert(entity);
            }
            return mensagem;
        }

        public Marca Selecionar(int id)
        {
            return _repository.GetById(id);
        }

        public Marca SelecionarPorNome(string nome)
        {
            return this.SelecionarTodos().Where(w => w.Nome == nome).FirstOrDefault();
        }

        public Marca SelecionarPorId(int id)
        {
            return this.SelecionarTodos().Where(w => w.Id == id).FirstOrDefault();
        }

        public IQueryable<Marca> SelecionarAtivos()
        {
            return this.SelecionarTodos().Where(w => w.Situacao == Situacao.Ativo);
        }

        public IQueryable<Marca> SelecionarTodos()
        {
            return _repository.GetAll();
        }

        public string ValidarDados(Marca entity)
        {
            string mensagem = "";

            if (entity.Nome.Trim() == "")
            {
                mensagem = "Nome não informado!";
            }
            else
            {
                using MarcaRepository marcaRepository = new(_db);
                if (entity.Id > 0)
                {
                    string nomeanterior = marcaRepository.Selecionar(entity.Id).Nome;

                    if (entity.Nome != nomeanterior)
                    {
                        mensagem = "Não é permitido a alteração do nome!";
                    }
                }
                else
                {
                    Marca marcaAux = marcaRepository.SelecionarPorNome(entity.Nome);

                    if (marcaAux != null)
                    {
                        mensagem = "Já existe uma Marca com esse nome!";
                    }
                }
            }

            return mensagem;
        }

        public static string ValidaExclusao()
        {
            return "Não é permitido a exclusão do registro!";
        }

    }
}
