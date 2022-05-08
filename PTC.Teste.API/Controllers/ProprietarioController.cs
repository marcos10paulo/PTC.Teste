using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PTC.Teste.Context;
using PTC.Teste.Dto;
using PTC.Teste.Entity;
using PTC.Teste.Repository;
using System.Collections.Generic;
using System.Linq;

namespace PTC.Teste.API.Controllers
{
    [Authorize]
    [Route("api/proprietario")]
    [ApiController]
    public class ProprietarioController : ControllerBase
    {
        private string _mensagem = "";

        [HttpPost("salvar")]
        public IActionResult Salvar([FromBody] Proprietario entity)
        {
            using (var context = new PTCTesteContext())
            {
                using var transacao = context.Database.BeginTransaction();
                using var repository = new ProprietarioRepository(context);

                if (entity.Id == 0)
                    _mensagem = repository.Incluir(entity);
                else
                    _mensagem = repository.Alterar(entity);

                if (_mensagem == "")
                    transacao.Commit();
                else
                    transacao.Rollback();
            }
            if (_mensagem != "")
            {
                return BadRequest(_mensagem);
            }
            return Ok();
        }

        [HttpDelete("excluir/{id}")]
        public IActionResult Excluir(int id)
        {
            using (var context = new PTCTesteContext())
            {
                using var transacao = context.Database.BeginTransaction();
                using var repository = new ProprietarioRepository(context);
                _mensagem = repository.Excluir(id);
                if (_mensagem == "")
                    transacao.Commit();
                else
                    transacao.Rollback();
            }
            if (_mensagem != "")
            {
                return BadRequest(_mensagem);
            }
            return Ok(); ;
        }

        [HttpGet("selecionar/{id}")]
        public ActionResult<Proprietario> Selecionar(int id)
        {
            using var repository = new ProprietarioRepository();
            return repository.Selecionar(id);
        }

        [HttpGet("selecionartodos")]
        public IEnumerable<Proprietario> SelecionarTodos()
        {
            using var repository = new ProprietarioRepository();
            return repository.SelecionarTodos();
        }

        [HttpPost("filtrar")]
        public IEnumerable<Proprietario> Filtrar([FromBody] FiltroDTO filtro)
        {
            using var repository = new ProprietarioRepository();
            return repository.Filtrar(filtro.Condicao);
        }

        [HttpGet("selecionarativos")]
        public IEnumerable<Proprietario> SelecionarAtivos()
        {
            using var repository = new ProprietarioRepository();
            return repository.SelecionarAtivos().ToList();
        }
    }
}
