using GestaoSalao.Models.Entity;
using GestaoSalao.Models.Intefaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace GestaoSalao.UI.Controllers
{
    public class UsuarioController : BaseController
    {
        #region Propriedades e Construtor

        private readonly IUsuarioServices _usuarioServices;

        public UsuarioController(IUsuarioServices usuarioServices)
        {
            _usuarioServices = usuarioServices;
        }

        #endregion

        public IActionResult Index()
        {
            return View(_usuarioServices.ConsultaTodos());
        }

        [HttpGet]
        public IActionResult Create()
        {
            ViewBag.Tipo = ListTipo();

            return PartialView("_Form", new Usuario() { Tipo = 1 });
        }

        [HttpPost]
        public IActionResult Create(Usuario model)
        {
            try
            {
                ViewBag.Tipo = ListTipo();

                var retorno = _usuarioServices.Inserir(model);
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
            ViewBag.Tipo = ListTipo();
            try
            {
                return PartialView("_Form", _usuarioServices.ConsultaPorId(id));
            }
            catch (Exception ex)
            {
                AddModelError(ex.Message, ex);
                throw;
            }
        }

        [HttpPost]
        public IActionResult Edit(Usuario model)
        {
            try
            {
                ViewBag.Tipo = ListTipo();

                var retorno = _usuarioServices.Atualizar(model);
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

        public List<SelectListItem> ListTipo()
        {
            return new List<SelectListItem>
            {
                new (){ Text = "Administrador", Value = "1"},
                new (){ Text = "Funcionário", Value = "2"},
                new (){ Text = "Cliente", Value = "3"},
            };
        }
    }
}