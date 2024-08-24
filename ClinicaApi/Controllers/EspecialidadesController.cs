using Clinica.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace ClinicaApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EspecialidadesController : ControllerBase
    {
        private readonly IEspecialidadServices _services;

        public EspecialidadesController(IEspecialidadServices services)
        {
            _services = services;
        }

        [HttpGet]
        public async Task<IActionResult> Get(string? filtroNombreEspecialidad, int page = 1, int rows = 5)
        {
            var response = await _services.ListAsync(filtroNombreEspecialidad, page, rows);

            return Ok(response);

        }

        [HttpGet("GetListaCompleta")]
        public async Task<IActionResult> GetListaCompleta()
        {
            var response = await _services.ListAsync();
            return Ok(response);
        }

        [HttpGet("id:int")]
        public async Task<IActionResult> Get(int id)
        {
            var response = await _services.FindByIdAsync(id);

            return response.Success ? Ok(response) : NotFound(response);    

        }

    }
}
