using GestaoSalao.Models.Entity;
using GestaoSalao.Models.Intefaces;
using GestaoSalao.UI.ExtensionWeb;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace GestaoSalao.UI.Controllers
{
    public class VendaController : BaseController
    {
        #region Propriedades e Construtor

        private readonly IVendaServices _vendaServices;
        private readonly IProdutoServices _produtoServices;
        private readonly IServicoServices _servicoServices;
        private readonly IUsuarioServices _usuarioServices;

        public VendaController(IVendaServices vendaServices, IProdutoServices produtoServices, IServicoServices servicoServices, IUsuarioServices usuarioServices)
        {
            _vendaServices = vendaServices;
            _produtoServices = produtoServices;
            _servicoServices = servicoServices;
            _usuarioServices = usuarioServices;
        }

        #endregion

        public IActionResult Index()
        {
            return View(ConsultarTodos());
        }

        [HttpGet]
        public IActionResult Create()
        {
            SetCollections();
            return PartialView("_Form", new Venda() { Valor = decimal.Zero });
        }

        [HttpPost]
        public IActionResult Create(Venda model)
        {
            try
            {
                model.IdVendedor = IdUsuarioLogado();
                model.DataOperacao = DateTime.Now;

                var retorno = _vendaServices.Inserir(model);
                if (!string.IsNullOrEmpty(retorno))
                {
                    SetCollections();
                    AddModelError("", retorno);
                    return PartialView("_Form", model);
                }
            }
            catch (Exception ex)
            {
                SetCollections();
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
                SetCollections();
                return PartialView("_Form", _vendaServices.ConsultaPorId(id));
            }
            catch (Exception ex)
            {
                AddModelError(ex.Message, ex);
                throw;
            }
        }

        [HttpPost]
        public IActionResult Edit(Venda model)
        {
            try
            {
                model.IdVendedor = IdUsuarioLogado();
                model.DataOperacao = DateTime.Now;

                var retorno = _vendaServices.Atualizar(model);
                if (!string.IsNullOrEmpty(retorno))
                {
                    SetCollections();
                    AddModelError("", retorno);
                    return PartialView("_Form", model);
                }
            }
            catch (Exception ex)
            {
                SetCollections();
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

        private void SetCollections()
        {
            ViewBag.Tipo = ListTipo();
            ViewBag.Produtos = ListProdutos();
            ViewBag.Servicos = ListServicos();
        }

        private List<SelectListItem> ListTipo()
        {
            return new List<SelectListItem>
            {
                new (){ Text = "Venda", Value = "1"},
                new (){ Text = "Serviço", Value = "2"}
            };
        }

        private SelectList ListProdutos()
        {
            var produtos = _produtoServices.ConsultaTodos();
            return produtos.ToSelectList(x => x.Id, w => w.Descricao);
        }

        private SelectList ListServicos()
        {
            var servicos = _servicoServices.ConsultaTodos();
            return servicos.ToSelectList(x => x.Id, w => w.Descricao);
        }

        private IEnumerable<Venda> ConsultarTodos()
        {
            var vendas = _vendaServices.ConsultaTodos();
            foreach (var venda in vendas)
            {
                var usuario = _usuarioServices.ConsultaPorId(venda.IdVendedor);
                venda.NomeVendedor = usuario.Nome;

                if (venda.Tipo == 1)
                {
                    var produto = _produtoServices.ConsultaPorId(venda.IdProduto ?? 0);
                    venda.DescricaoOperacao = produto?.Nome;
                }
                else
                {
                    var servico = _servicoServices.ConsultaPorId(venda.IdServico ?? 0);
                    venda.DescricaoOperacao = servico?.Nome;
                }
            }

            return vendas;
        }
    }
}