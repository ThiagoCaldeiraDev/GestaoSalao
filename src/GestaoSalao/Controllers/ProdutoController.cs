using GestaoSalao.Models.Entity;
using GestaoSalao.Models.Intefaces;
using Microsoft.AspNetCore.Mvc;

namespace GestaoSalao.UI.Controllers
{
    public class ProdutoController : BaseController
    {
        #region Propriedades e Construtor

        private readonly IProdutoServices _ProdutoServices;

        public ProdutoController(IProdutoServices ProdutoServices)
        {
            _ProdutoServices = ProdutoServices;
        }

        #endregion

        public IActionResult Index()
        {
            return View(_ProdutoServices.ConsultaTodos());
        }

        [HttpGet]
        public IActionResult Create()
        {
            return PartialView("_Form", new Produto() { Valor = decimal.Zero });
        }

        [HttpPost]
        public IActionResult Create(Produto model)
        {
            try
            {
                var retorno = _ProdutoServices.Inserir(model);
                if (!string.IsNullOrEmpty(retorno))
                {
                    AddModelError("", retorno);
                    return PartialView("_Form", model);
                }
            }
            catch (Exception ex)
            {
                AddModelError(ex.Message, ex);
                return PartialView("_Form", model);
            }

            return Json(new
            {
                sucesso = true,
                model.Id,
                Guid = model.Guid.ToString()
            });
        }

        [HttpGet]
        public ActionResult Edit(int id)
        {
            try
            {
                return PartialView("_Form", _ProdutoServices.ConsultaPorId(id));
            }
            catch (Exception ex)
            {
                AddModelError(ex.Message, ex);
                throw;
            }
        }

        [HttpPost]
        public IActionResult Edit(Produto model)
        {
            try
            {
                var retorno = _ProdutoServices.Atualizar(model);
                if (!string.IsNullOrEmpty(retorno))
                {
                    AddModelError("", retorno);
                    return PartialView("_Form", model);
                }
            }
            catch (Exception ex)
            {
                AddModelError(ex.Message, ex);
                return PartialView("_Form", model);
            }

            return Json(new
            {
                sucesso = true,
                model.Id,
                Guid = model.Guid.ToString()
            });
        }
    }
}