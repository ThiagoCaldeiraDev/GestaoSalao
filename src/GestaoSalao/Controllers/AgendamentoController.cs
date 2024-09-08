using GestaoSalao.Models.Entity;
using GestaoSalao.Models.Intefaces;
using GestaoSalao.Models.ViewModels.Entidades;
using GestaoSalao.UI.ExtensionWeb;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace GestaoSalao.UI.Controllers
{
    public class AgendamentoController : BaseController
    {
        #region Propriedades e Construtor

        private readonly IVendaServices _vendaServices;
        private readonly IProdutoServices _produtoServices;
        private readonly IServicoServices _servicoServices;
        private readonly IUsuarioServices _usuarioServices;
        private readonly IAgendamentoServices _agendamentoServices;

        public AgendamentoController(IVendaServices vendaServices, IProdutoServices produtoServices, IServicoServices servicoServices, IUsuarioServices usuarioServices, IAgendamentoServices agendamentoServices)
        {
            _vendaServices = vendaServices;
            _produtoServices = produtoServices;
            _servicoServices = servicoServices;
            _usuarioServices = usuarioServices;
            _agendamentoServices = agendamentoServices;
        }

        #endregion

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Buscar()
        {
            var agendamentos = IdUsuarioCliente() ? ConsultarPorUsuario(IdUsuarioLogado()) : ConsultarTodos();

            var events = agendamentos.Select(x => new AgendamentoViewModel
            {
                IdAgendamento = x.Id,
                Start = x.DataAgendamentoInicial,
                End = x.DataAgendamentoFinal,
                Profissional = x.NomeProfissional,
                Servico = x.NomeServico,
                AllDay = false,
                Title = x.NomeServico,
                Color = x.DataAgendamentoInicial > DateTime.Now ? "#FFA550" : "#008000",
                Time = string.Empty
            });

            return Json(events);
        }

        [HttpGet]
        public ActionResult Create()
        {
            try
            {
                SetCollections();
                return PartialView("_Form");
            }
            catch (Exception ex)
            {
                AddModelError(ex.Message, ex);
                throw;
            }
        }

        [HttpPost]
        public IActionResult Create(Agendamento model)
        {
            try
            {
                model.IdUsuario = IdUsuarioLogado();

                var servico = ConsultarServicoPorId(model.IdServico);
                model.DataAgendamentoFinal = model.DataAgendamentoInicial.AddHours(servico.Tempo.Hour).AddMinutes(servico.Tempo.Minute);

                var retorno = _agendamentoServices.Inserir(model);
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
        public IActionResult Detalhes(int idAgendamento)
        {
            try
            {
                return PartialView("_FormVisualizar", ConsultarPorId(idAgendamento));
            }
            catch (Exception ex)
            {
                AddModelError(ex.Message, ex);
                throw;
            }
        }

        private IEnumerable<Agendamento> ConsultarPorUsuario(int idUsuario)
        {
            var agendamentos = _agendamentoServices.ConsultaTodos().Where(x => x.IdUsuario == idUsuario);
            foreach (var agendamento in agendamentos)
            {
                var profissional = _usuarioServices.ConsultaPorId(agendamento.IdProfissional);
                agendamento.NomeProfissional = profissional?.Nome;

                var servico = _servicoServices.ConsultaPorId(agendamento.IdServico);
                agendamento.NomeServico = servico?.Nome;
            }

            return agendamentos;
        }

        private Agendamento ConsultarPorId(int idAgendamento)
        {
            var agendamento = _agendamentoServices.ConsultaPorId(idAgendamento);

            var cliente = _usuarioServices.ConsultaPorId(agendamento.IdUsuario);
            agendamento.NomeCliente = cliente?.Nome;

            var profissional = _usuarioServices.ConsultaPorId(agendamento.IdProfissional);
            agendamento.NomeProfissional = profissional?.Nome;

            var servico = _servicoServices.ConsultaPorId(agendamento.IdServico);
            agendamento.NomeServico = servico?.Nome;

            return agendamento;
        }

        private IEnumerable<Agendamento> ConsultarTodos()
        {
            var agendamentos = _agendamentoServices.ConsultaTodos();
            foreach (var agendamento in agendamentos)
            {
                var profissional = _usuarioServices.ConsultaPorId(agendamento.IdProfissional);
                agendamento.NomeProfissional = profissional?.Nome;

                var servico = _servicoServices.ConsultaPorId(agendamento.IdServico);
                agendamento.NomeServico = servico?.Nome;
            }

            return agendamentos;
        }

        private Servico ConsultarServicoPorId(int idServico)
        {
            return _servicoServices.ConsultaPorId(idServico);
        }

        private void SetCollections()
        {
            ViewBag.Profissional = ListProfissionais();
            ViewBag.Servicos = ListServicos();
        }

        private SelectList ListServicos()
        {
            var servicos = _servicoServices.ConsultaTodos();
            return servicos.ToSelectList(x => x.Id, w => w.Descricao);
        }

        private SelectList ListProfissionais()
        {
            var servicos = _usuarioServices.ConsultaTodos().Where(x => x.Tipo != 3);
            return servicos.ToSelectList(x => x.Id, w => w.Nome);
        }
    }
}