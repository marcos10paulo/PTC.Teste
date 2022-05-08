using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using PTC.Teste.Context;
using PTC.Teste.Dto;
using PTC.Teste.Dto.Configuration;
using PTC.Teste.Entity;
using PTC.Teste.Repository;
using RabbitMQ.Client;
using System.Collections.Generic;
using System.Linq;

namespace PTC.Teste.API.Controllers
{
    [Authorize]
    [Route("api/veiculo")]
    [ApiController]
    public class VeiculoController : ControllerBase
    {
        private string _mensagem = "";
        private readonly ConnectionFactory _factory;
        private readonly RabbitMqConfiguration _configuration;

        public VeiculoController(IOptions<RabbitMqConfiguration> option)
        {
            _configuration = option.Value;

            _factory = new ConnectionFactory
            {
                HostName = _configuration.Host
            };
        }

        [HttpPost("salvar")]
        public IActionResult Salvar([FromBody] Veiculo entity)
        {
            using (var context = new PTCTesteContext())
            {
                using var transacao = context.Database.BeginTransaction();
                using var repository = new VeiculoRepository(context);

                if (entity.Id == 0)
                {
                    _mensagem = repository.Incluir(entity);

                    if (_mensagem.Trim() == "")
                    {
                        using ProprietarioRepository proprietarioRepository = new(context);
                        Proprietario proprietario = proprietarioRepository.Selecionar(entity.ProprietarioId);

                        using MarcaRepository marcaRepository = new(context);
                        Marca marca = marcaRepository.Selecionar(entity.MarcaId);

                        if (proprietario != null && marca != null)
                        {
                            new EnviarEmailMensageria(_factory).EnviarEmail(new EnviarEmailVeiculoDto()
                            {
                                Email = proprietario.Email,
                                Marca = marca.Nome,
                                Proprietario = proprietario.Nome,
                                Modelo = entity.Modelo,
                                VeiculoId = entity.Id
                            });
                        }
                    }
                }
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
                using var repository = new VeiculoRepository(context);
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
        public ActionResult<Veiculo> Selecionar(int id)
        {
            using var repository = new VeiculoRepository();
            return repository.Selecionar(id);
        }

        [HttpGet("selecionartodos")]
        public IEnumerable<Veiculo> SelecionarTodos()
        {
            using var repository = new VeiculoRepository();
            return repository.SelecionarTodos();
        }

        [HttpPost("filtrar")]
        public IEnumerable<VeiculoDto> Filtrar([FromBody] FiltroDTO filtro)
        {
            using var repository = new VeiculoRepository();
            return repository.FiltrarDto(filtro.Condicao).ToList();
        }
    }
}
