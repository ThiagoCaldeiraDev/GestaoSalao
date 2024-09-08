using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using GestaoSalao.Models.Intefaces;

namespace GestaoSalao.UI.Controllers
{
    public class LoginController : Controller
    {
        #region Propriedades e Construtor
        private readonly IConfiguration _configuration;
        private readonly IUsuarioServices _usuarioServices;

        public LoginController(IConfiguration configuration, IUsuarioServices usuarioServices)
        {
            _configuration = configuration;
            _usuarioServices = usuarioServices;
        }
        #endregion

        public IActionResult Index() => View();

        [HttpPost]
        public async Task<IActionResult> Logon(string cpf, string senha)
        {
            var usuario = _usuarioServices.ExisteUsuario(cpf, senha);
            if (usuario == null)
            {
                ViewBag.Erro = "Login ou senha incorretos.";
                return View("Index");
            }

            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, usuario.Id.ToString()),
                new Claim(ClaimTypes.Name, usuario.Nome),
                new Claim(ClaimTypes.Email, usuario.Tipo.ToString()),
            };

            var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);

            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(claimsIdentity));

            return RedirectToAction("Index", "Home");
        }

        public IActionResult Logout()
        {
            HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);

            return Redirect("../");
        }
    }
}