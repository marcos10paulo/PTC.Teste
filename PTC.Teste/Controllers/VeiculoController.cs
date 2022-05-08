using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PTC.Teste.Common.Interface;
using PTC.Teste.Dto;
using PTC.Teste.Entity;
using PTC.Teste.Models;
using PTC.Teste.Models.Marca;
using PTC.Teste.Models.Proprietario;
using PTC.Teste.Models.Veiculo;
using System.Collections.Generic;
using System.Linq;

namespace PTC.Teste.Controllers
{
    public class VeiculoController : BaseController
    {
        private readonly List<MarcaViewModel> _marcas;
        private readonly List<ProprietarioViewModel> _proprietarios;

        public VeiculoController(IRequest request, IFunctions functions, IHttpContextAccessor contextAccessor) : base(request, functions, contextAccessor)
        {
            
            _marcas = new List<MarcaViewModel>();
            IEnumerable<Marca> marcas = _request.Get<IEnumerable<Marca>>("marca", "selecionarativos");

            if (marcas.Any())
            {
                foreach (Marca item in marcas)
                {
                    _marcas.Add(new MarcaViewModel()
                    {
                        Id = item.Id,
                        Nome = item.Nome
                    });
                }
            }

            _proprietarios = new List<ProprietarioViewModel>();
            IEnumerable<Proprietario> proprietarios = _request.Get<IEnumerable<Proprietario>>("proprietario", "selecionarativos");

            if (proprietarios.Any())
            {
                foreach (Proprietario item in proprietarios)
                {
                    _proprietarios.Add(new ProprietarioViewModel()
                    {
                        Id = item.Id,
                        Nome = item.Nome
                    });
                }
            }
        }

        [HttpGet]
        public IActionResult Index()
        {
            return View(new List<VeiculoViewModel>());
        }


        [HttpPost]
        public IActionResult Index(DefaultIndexViewModel model)
        {
            List<VeiculoViewModel> models = new();

            if (ModelState.IsValid)
            {
                string sCondicao = base.GerarCondicaoFiltro<VeiculoDto>(model.Filtro);

                IEnumerable<VeiculoDto> items = _request.Post<IEnumerable<VeiculoDto>>("Veiculo", "filtrar", sCondicao, null);

                if (items != null)
                {
                    foreach (VeiculoDto item in items)
                    {
                        models.Add(new VeiculoViewModel()
                        {
                            Id = item.Id,
                            ProprietarioId = item.ProprietarioId,
                            Renavam = item.Renavam,
                            MarcaId = item.MarcaId,
                            Modelo = item.Modelo,
                            AnoFabricacao = item.AnoFabricacao,
                            AnoModelo = item.AnoModelo,
                            Quilometragem = item.Quilometragem,
                            Valor = item.Valor,
                            SituacaoVeiculo = item.SituacaoVeiculo,
                            Proprietario = item.Proprietario,
                            Marca = item.Marca

                        });
                    }
                }
            }

            return View(models);
        }


        [HttpGet]
        public IActionResult AddOrEdit(int id)
        {
            if (id > 0)
            {
                Veiculo item = _request.Get<Veiculo>("Veiculo", $"selecionar/{id}");

                if (item != null)
                {
                    VeiculoViewModel model = new()
                    {
                        Id = item.Id,
                        ProprietarioId = item.ProprietarioId,
                        Renavam = item.Renavam,
                        MarcaId = item.MarcaId,
                        Modelo = item.Modelo,
                        AnoFabricacao = item.AnoFabricacao,
                        AnoModelo = item.AnoModelo,
                        Quilometragem = item.Quilometragem,
                        Valor = item.Valor,
                        SituacaoVeiculo = item.SituacaoVeiculo
                    };

                    model.Marcas = _marcas;
                    model.Proprietarios = _proprietarios;

                    return View(model);
                }
            }

            return View(new VeiculoViewModel() { Proprietarios = _proprietarios, Marcas = _marcas });
        }


        [HttpPost]
        public IActionResult AddOrEdit(VeiculoViewModel model)
        {
            if (ModelState.IsValid)
            {
                string mensagem = _request.Post<Veiculo>(new Veiculo()
                {
                    Id = model.Id,
                    ProprietarioId = model.ProprietarioId,
                    Renavam = model.Renavam,
                    MarcaId = model.MarcaId,
                    Modelo = model.Modelo,
                    AnoFabricacao = model.AnoFabricacao.Value,
                    AnoModelo = model.AnoModelo.Value,
                    Quilometragem = model.Quilometragem.Value,
                    Valor = model.Valor.Value,
                    SituacaoVeiculo = model.SituacaoVeiculo


                }, "Veiculo", "salvar");

                if (!string.IsNullOrEmpty(mensagem))
                {
                    ViewBag.Error = mensagem;
                    model.Proprietarios = _proprietarios;
                    model.Marcas = _marcas;

                    return View(model);
                }

                return RedirectToAction("Index");
            }

            model.Proprietarios = _proprietarios;
            model.Marcas = _marcas;

            return View(model);
        }

        public IActionResult Delete(int id)
        {
            if (id > 0)
            {
                string mensagem = _request.Delete("Veiculo", "excluir", id);

                if (!string.IsNullOrEmpty(mensagem))
                {
                    ViewBag.Error = mensagem;
                    TempData["Error"] = mensagem;

                    return RedirectToAction("AddOrEdit", new { id });
                }

                return RedirectToAction("Index");
            }

            return View();
        }
    }
}