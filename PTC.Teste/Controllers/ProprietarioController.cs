using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PTC.Teste.Common.Interface;
using PTC.Teste.Entity;
using PTC.Teste.Models;
using PTC.Teste.Models.Proprietario;
using System.Collections.Generic;

namespace PTC.Teste.Controllers
{
    public class ProprietarioController : BaseController
    {
        public ProprietarioController(IRequest request, IFunctions functions, IHttpContextAccessor contextAccessor) : base(request, functions, contextAccessor)
        {

        }

        [HttpGet]
        public IActionResult Index()
        {
            return View(new List<ProprietarioViewModel>());
        }


        [HttpPost]
        public IActionResult Index(DefaultIndexViewModel model)
        {
            List<ProprietarioViewModel> models = new();

            if (ModelState.IsValid)
            {
                string sCondicao = base.GerarCondicaoFiltro<ProprietarioViewModel>(model.Filtro);

                IEnumerable<Proprietario> items = _request.Post<IEnumerable<Proprietario>>("Proprietario", "filtrar", sCondicao, null);

                if (items != null)
                {
                    foreach (Proprietario item in items)
                    {
                        models.Add(new ProprietarioViewModel()
                        {
                            Id = item.Id,
                            Nome = item.Nome,
                            Status = item.Status,
                            Documento = item.Documento,
                            Cep = item.Cep,
                            Endereco = item.Endereco,
                            Bairro = item.Bairro,
                            Cidade = item.Cidade,
                            Email = item.Email,
                            Estado = item.Estado,
                            Numero = item.Numero,

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
                Proprietario item = _request.Get<Proprietario>("Proprietario", $"selecionar/{id}");

                if (item != null)
                {
                    ProprietarioViewModel model = new()
                    {
                        Id = item.Id,
                        Nome = item.Nome,
                        Status = item.Status,
                        Documento = item.Documento,
                        Cep = item.Cep,
                        Endereco = item.Endereco,
                        Bairro = item.Bairro,
                        Cidade = item.Cidade,
                        Email = item.Email,
                        Estado = item.Estado,
                        Numero = item.Numero,
                    };

                    return View(model);
                }
            }

            return View(new ProprietarioViewModel());
        }


        [HttpPost]
        public IActionResult AddOrEdit(ProprietarioViewModel model)
        {
            if (ModelState.IsValid)
            {
                string mensagem = _request.Post<Proprietario>(new Proprietario()
                {
                    Id = model.Id,
                    Nome = model.Nome,
                    Status = model.Status,
                    Documento = _functions.RemoveNaoNumericos(model.Documento),
                    Cep = model.Cep,
                    Endereco = model.Endereco,
                    Bairro = model.Bairro,
                    Cidade = model.Cidade,
                    Email = model.Email,
                    Estado = model.Estado,
                    Numero = model.Numero,


                }, "Proprietario", "salvar");

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
                string mensagem = _request.Delete("Proprietario", "excluir", id);

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
