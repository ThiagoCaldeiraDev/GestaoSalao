using GestaoSalao.Models.Entity;
using GestaoSalao.Models.Intefaces;
using Microsoft.AspNetCore.Mvc;

namespace GestaoSalao.UI.Controllers
{
    public class ServicoController : BaseController
    {
        #region Propriedades e Construtor

        private readonly IServicoServices _servicoServices;

        public ServicoController(IServicoServices servicoServices)
        {
            _servicoServices = servicoServices;
        }

        #endregion

        public IActionResult Index()
        {
            return View(_servicoServices.ConsultaTodos());
        }

        [HttpGet]
        public IActionResult Create()
        {
            return PartialView("_Form", new Servico() { Valor = decimal.Zero });
        }

        [HttpPost]
        public IActionResult Create(Servico model)
        {
            try
            {
                var retorno = _servicoServices.Inserir(model);
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
                return PartialView("_Form", _servicoServices.ConsultaPorId(id));
            }
            catch (Exception ex)
            {
                AddModelError(ex.Message, ex);
                throw;
            }
        }

        [HttpPost]
        public IActionResult Edit(Servico model)
        {
            try
            {
                var retorno = _servicoServices.Atualizar(model);
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