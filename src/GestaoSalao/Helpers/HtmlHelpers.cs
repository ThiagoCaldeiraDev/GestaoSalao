using Microsoft.AspNetCore.Mvc.Rendering;
using System.Security.Claims;

namespace GestaoSalao.UI.Helpers
{
    public static class HtmlHelpers
    {
        private static readonly IHttpContextAccessor _httpContextAccessor = new HttpContextAccessor();

        public static string IsSelected(this IHtmlHelper htmlHelper, string controller, string? actions = null, string cssClass = "active")
        {
            string currentAction = htmlHelper.ViewContext.RouteData.Values["action"] as string;
            string currentController = htmlHelper.ViewContext.RouteData.Values["controller"] as string;

            ICollection<string> acceptedActions = (actions ?? currentAction).Split(',');
            ICollection<string> acceptedControllers = (controller ?? currentController).Split(',');

            return acceptedActions.Contains(currentAction) && acceptedControllers.Contains(currentController) ?
                cssClass : String.Empty;
        }

        public static string PageClass(this IHtmlHelper htmlHelper)
        {
            return (string)htmlHelper.ViewContext.RouteData.Values["action"];
        }

        public static bool IdUsuarioNaoCliente(this IHtmlHelper htmlHelper)
        {
            var id = _httpContextAccessor?.HttpContext?.User?.Claims?.FirstOrDefault(it => it.Type == ClaimTypes.Email)?.Value ?? "0";
            return id != "3";
        }

        public static bool IdUsuarioAdmin(this IHtmlHelper htmlHelper)
        {
            var id = _httpContextAccessor?.HttpContext?.User?.Claims?.FirstOrDefault(it => it.Type == ClaimTypes.Email)?.Value ?? "0";
            return id == "1";
        }
    }
}
