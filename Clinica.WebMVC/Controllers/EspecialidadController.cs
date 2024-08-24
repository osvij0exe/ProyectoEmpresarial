using Clinica.WebMVC.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Clinica.WebMVC.Controllers
{
    public class EspecialidadController : Controller
    {
        private readonly IEspecialidadProxy _proxy;

        public EspecialidadController(IEspecialidadProxy proxy)
        {
            _proxy = proxy;
        }

        //Get
        public async Task<IActionResult> Index()
        {

            return View( await _proxy.ListAsync());
        }
    }
}
