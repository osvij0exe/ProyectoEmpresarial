using Clinica.ViewModels;
using Clinica.WebMVC.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Clinica.WebMVC.Controllers
{
    public class DelegacionesController : Controller
    {
        private readonly IUbigeoProxy _ubigeoProxy;

        public DelegacionesController(IUbigeoProxy ubigeoProxy)
        {
            _ubigeoProxy = ubigeoProxy;
        }

        public async Task<IActionResult >Cargar(string codigoCiudad) 
        {
            _ubigeoProxy.UrlBase = $"{HttpContext.Request.Scheme}://{HttpContext.Request.Host}";

            var delegaciones = new List<DelegacionModel>()
            {

                new()
                {
                    Codigo = "00",
                    Nombre = "-Seleccione"
                }

            };
            delegaciones.AddRange( await _ubigeoProxy.ListarDelegaciones(codigoCiudad));



            return Json(delegaciones); 
        }
    }
}
