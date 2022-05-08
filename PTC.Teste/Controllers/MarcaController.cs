using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PTC.Teste.Common.Interface;
using PTC.Teste.Entity;
using PTC.Teste.Models;
using PTC.Teste.Models.Marca;
using System.Collections.Generic;

namespace PTC.Teste.Controllers
{
    public class MarcaController : BaseController
    {
        public MarcaController(IRequest request, IFunctions functions, IHttpContextAccessor contextAccessor) : base(request, functions, contextAccessor)
        {

        }

        [HttpGet]
        public IActionResult Index()
        {
            return View(new List<MarcaViewModel>());
        }


        [HttpPost]
        public IActionResult Index(DefaultIndexViewModel model)
        {
            List<MarcaViewModel> models = new List<MarcaViewModel>();

            if (ModelState.IsValid)
            {
                string sCondicao = base.GerarCondicaoFiltro<MarcaViewModel>(model.Filtro);

                IEnumerable<Marca> items = _request.Post<IEnumerable<Marca>>("Marca", "filtrar", sCondicao, null);                

                if (items != null)
                {
                    foreach (Marca item in items)
                    {
                        models.Add(new MarcaViewModel()
                        {
                            Id = item.Id,
                            Nome = item.Nome,
                            Situacao = item.Situacao
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
                Marca item = _request.Get<Marca>("Marca", $"selecionar/{id}");

                if (item != null)
                {
                    MarcaViewModel model = new MarcaViewModel()
                    {
                        Id = item.Id,
                        Nome = item.Nome,
                        Situacao = item.Situacao

                    };

                    return View(model);
                }
            }

            return View(new MarcaViewModel());
        }


        [HttpPost]
        public IActionResult AddOrEdit(MarcaViewModel model)
        {
            if (ModelState.IsValid)
            {
                string mensagem = _request.Post<Marca>(new Marca()
                {
                    Id = model.Id,
                    Nome = model.Nome,
                    Situacao = model.Situacao

                }, "Marca", "salvar");

                if (!string.IsNullOrEmpty(mensagem))
                {
                    ViewBag.Error = mensagem;

                    return View(model);
                }

                return RedirectToAction("Index");
            }

            return View(model);
        }

        public IActionResult Delete(int id)
        {
            if (id > 0)
            {
                string mensagem = _request.Delete("Marca", "excluir", id);

                if (!string.IsNullOrEmpty(mensagem))
                {
                    ViewBag.Error = mensagem;
                    TempData["Error"] = mensagem;

                    return RedirectToAction("AddOrEdit", new { id = id });
                }

                return RedirectToAction("Index");
            }

            return View();
        }
    }
}
