using GestaoSalao.Models.Entity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Security.Claims;

namespace GestaoSalao.UI.Controllers
{
    public class BaseController : Controller
    {
        private readonly IHttpContextAccessor _httpContextAccessor = new HttpContextAccessor();

        protected void AddModelError(string key, Exception ex, bool logError = true)
        {
            ModelState.AddModelError(key, ex.Message);
        }

        protected void AddModelError(string key, string message, bool logError = true)
        {
            ModelState.AddModelError(key, message);
        }

        protected void AddModelError(IEnumerable<string> erros)
        {
            foreach (var erro in erros)
            {
                ModelState.AddModelError("", erro);
            }
        }

        public override void OnActionExecuting(ActionExecutingContext context)
        {
            var usuario = _httpContextAccessor?.HttpContext?.User?.Claims?.FirstOrDefault(it => it.Type == ClaimTypes.Name)?.Value;

            if (string.IsNullOrEmpty(usuario))
            {
                context.Result = new RedirectToRouteResult(
                    new RouteValueDictionary(
                        new
                        {
                            area = "",
                            controller = "Login",
                            action = ""
                        }));
            }
        }

        protected int IdUsuarioLogado()
        {
            var id = _httpContextAccessor?.HttpContext?.User?.Claims?.FirstOrDefault(it => it.Type == ClaimTypes.NameIdentifier)?.Value ?? "0";
            return int.Parse(id);
        }

        protected bool IdUsuarioCliente()
        {
            var id = _httpContextAccessor?.HttpContext?.User?.Claims?.FirstOrDefault(it => it.Type == ClaimTypes.Email)?.Value ?? "0";
            return id == "3";
        }
    }
}