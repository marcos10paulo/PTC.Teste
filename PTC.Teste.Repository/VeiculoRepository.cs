using PTC.Teste.Context;
using PTC.Teste.Dto;
using PTC.Teste.Entity;
using PTC.Teste.Repository.Repository;
using System;
using System.Linq;
using System.Linq.Dynamic.Core;

namespace PTC.Teste.Repository
{
    public class VeiculoRepository : IDefaultRepository<Veiculo>, IDisposable
    {
        private readonly PTCTesteContext _db = new();
        private readonly IRepository<Veiculo> _repository;

        public VeiculoRepository(PTCTesteContext context = null)
        {
            _repository = new Repository<Veiculo>(context ?? new PTCTesteContext());
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

        public string Alterar(Veiculo entity)
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

        public IQueryable<Veiculo> Filtrar(string condicao)
        {
            return _repository.Filter(condicao);
        }

        public IQueryable<VeiculoDto> FiltrarDto(string condicao)
        {
            return this.SelecionarTodosDto().Where(condicao);
        }

        public string Incluir(Veiculo entity)
        {
            string mensagem = ValidarDados(entity);

            if (mensagem == "")
            {
                mensagem = _repository.Insert(entity);
            }
            return mensagem;
        }

        public Veiculo Selecionar(int id)
        {
            return _repository.GetById(id);
        }

        public IQueryable<Veiculo> SelecionarTodos()
        {
            return _repository.GetAll();
        }

        public Veiculo SelecionarPorRenavam(string renavam)
        {
            return this.SelecionarTodos().Where(w => w.Renavam == renavam).FirstOrDefault();
        }

        public IQueryable<VeiculoDto> SelecionarTodosDto()
        {

            var lista = (from v in _db.Veiculos
                         join p in _db.Proprietarios on v.ProprietarioId equals p.Id
                         join m in _db.Marcas on v.MarcaId equals m.Id
                         select new VeiculoDto
                         {
                             Id = v.Id,
                             ProprietarioId = v.ProprietarioId,
                             Renavam = v.Renavam,
                             MarcaId = v.MarcaId,
                             Modelo = v.Modelo,
                             AnoFabricacao = v.AnoFabricacao,
                             AnoModelo = v.AnoModelo,
                             Quilometragem = v.Quilometragem,
                             Valor = v.Valor,
                             SituacaoVeiculo = v.SituacaoVeiculo,
                             Proprietario = p.Nome,
                             Marca = m.Nome
                         });

            return lista;
        }

        public string ValidarDados(Veiculo entity)
        {
            string mensagem = "";

            if (entity.ProprietarioId == 0)
            {
                mensagem = "Proprietário não informado!";
            }
            else if (entity.Renavam.Trim() == "")
            {
                mensagem = "Renavam não informado!";
            }
            else if (entity.MarcaId == 0)
            {
                mensagem = "Marca não informada!";
            }
            else if (entity.Modelo.Trim() == "")
            {
                mensagem = "Renavam não informado!";
            }
            else if (entity.AnoFabricacao == 0)
            {
                mensagem = "Ano de fabricação não informado!";
            }
            else if (entity.AnoModelo == 0)
            {
                mensagem = "Ano modelo não informado!";
            }
            else if (entity.Quilometragem == 0)
            {
                mensagem = "Quilometragem não informado!";
            }
            else if (entity.Valor == 0)
            {
                mensagem = "Valor não informado!";
            }
            else
            {
                VeiculoRepository veiculoRepository = new(_db);
                Veiculo veiculoAux = veiculoRepository.SelecionarPorRenavam(entity.Renavam);

                if (veiculoAux != null && veiculoAux.Id != entity.Id)
                {
                    mensagem = $"Renavam já cadastrado anteriormente!";
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
