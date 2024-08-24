using Clinica.Models.Request;
using Clinica.Repositories.Implementacion.AdoRepository;
using Clinica.Repositories.Interfaces.IAdoRepository;
using Microsoft.AspNetCore.Mvc;

namespace ClinicaApi.Controllers.AdoContoller
{
    [ApiController]
    [Route("api/[controller]")]
    public class RecetaAdoController : ControllerBase
    {
        private readonly IRecetarioAdoRepocsitory _repocsitory;

        public RecetaAdoController(IRecetarioAdoRepocsitory repocsitory)
        {
            _repocsitory = repocsitory;
        }


        [HttpPost]
        public async Task<IActionResult> PostReceta(RecetarioDTORequest request)
        {
            var response = await _repocsitory.AddAdoRecetarioAsync(request);
            
            return Ok(response);

        }
        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetById(int id)
        {
            var response = await _repocsitory.FindRecetabyId(id);

            return response.Success ? Ok(response) : NotFound(response);
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> DeleteRecetaAsync(int id)
        {
            var response = await _repocsitory.DeletRecetaAdoAsync(id);

            return response.Success ? Ok(response) : BadRequest(response);
        }

        [HttpPatch("{id:int}")]
        public async Task<IActionResult> ReactivarRecetaAsync(int id)
        {
            var response = await _repocsitory.ReactivarRecetaAdoAsync(id);

            return response.Success ? Ok(response) : BadRequest(response);

        }

        [HttpGet("ListarEliminados")]
        public async Task<IActionResult> GetDeleteRecetaLista()
        {
            var response = await _repocsitory.ListDeleteRecetaAdoAync();

            return response.Success ? Ok(response) : BadRequest(response);
        }

    }
}
