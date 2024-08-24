using Clinica.Common;
using Clinica.Models.Request;
using Clinica.Models.Response;
using Clinica.WebMVC.Models;
using Clinica.WebMVC.Services.Interfaces;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace Clinica.WebMVC.Controllers
{
    public class UserController : Controller
    {
        private readonly IUserProxy _proxy;
        private readonly IUbigeoProxy _ubigeoProxy;

        public UserController(IUserProxy proxy, IUbigeoProxy ubigeoProxy)
        {
            _proxy = proxy;
            _ubigeoProxy = ubigeoProxy;
        }

        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LogingDTORequest modelo)
        {

            try
            {
                var response = await _proxy.LoginAsync(modelo);


                if (response.Success)
                {
                    // proceso de login
                    var handler = new JwtSecurityTokenHandler();
                    var jwt = handler.ReadJwtToken(response.Token);

                    // Leer los claims

                    var identity = new ClaimsIdentity(CookieAuthenticationDefaults.AuthenticationScheme);

                    identity.AddClaims(jwt.Claims);

                    var principal = new ClaimsPrincipal(identity);

                    await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal);

                    //Guardamos la sesion
                    HttpContext.Session.SetString(Constantes.JwtToken, response.Token);
                    return RedirectToAction("Index", "Home");

                }

                ModelState.AddModelError("ErrorMessage", response.ErrorMessage ?? "Error");
                return View(modelo);

            }
            catch (Exception ex)
            {

                ModelState.AddModelError("ErrorMessage", ex.Message);
                return View(modelo);
            }



        }


        public async Task<IActionResult> Register()
        {
            // Capturamos url para los Json del Frontend
            _ubigeoProxy.UrlBase = $"{HttpContext.Request.Scheme}://{HttpContext.Request.Host}";

            var vm = new RegisterViewModel();

            var listaCiudades = await _ubigeoProxy.ListarCiudades();

            vm.ListaCiudades = new List<SelectListItem>(listaCiudades.Select(p => new SelectListItem(p.Nombre,p.Codigo)));

            return View(vm);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Registet(RegisterViewModel modelo)
        {
            try
            {
                var response = await _proxy.RegisterAsync(modelo.Input);
                if(response.Success)
                {
                    return RedirectToAction(nameof(Login));
                }
                ModelState.AddModelError("ErrorMessage", response.ErrorMessage ?? "Error");
                return View(modelo);
            }
            catch (Exception ex)
            {

                ModelState.AddModelError("ErrorMessage", ex.Message);
                return View(modelo);
            }
          
        }

        public async Task<IActionResult> Logout()

        {
            await HttpContext.SignOutAsync();
            HttpContext.Session.SetString(Constantes.JwtToken, string.Empty);

            return RedirectToAction("Index", "Home");
        }
    }
}
