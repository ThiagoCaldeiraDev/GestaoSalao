using Microsoft.AspNetCore.Mvc;

namespace GestaoSalao.UI.Controllers
{
    public class HomeController : BaseController
    {
        public IActionResult Index() => View();
    }
}