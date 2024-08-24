using Clinica.Models.Request;
using Clinica.Models.Response;
using Clinica.Models.Response.MedicoResponse;
using Clinica.Repositories.Interfaces.IAdoRepository;
using Microsoft.AspNetCore.Mvc;

namespace ClinicaApi.Controllers.AdoContoller
{
    [ApiController]
    [Route("api/[controller]")]
    public class AdoMedicoController : ControllerBase
    {
        private readonly IAdoMedicoRepository _repository;

        public AdoMedicoController(IAdoMedicoRepository repository)
        {
            _repository = repository;
        }

        [HttpGet]
        public async Task<IActionResult> GetMedico(string? nombres, string? apellidos, string? cedulaProfecional, string? especialidad)
        {
            var response = await _repository.GetAdoMedicosAsync(nombres, apellidos, cedulaProfecional, especialidad);

            return Ok(response);
        }
        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetById(int id)
        {
            var response = await _repository.FindByIdAsync(id);

            return response.Success ? Ok(response) : NotFound(response);
        }
        [HttpPost]
        public async Task<IActionResult> PostMedico(MedicoDTORequest request)
        {
            var response = await _repository.AddMedicoAsync(request);

            return response.Success ? Ok(response) : BadRequest(response);
        }
        [HttpPut("{id:int}")]
        public async Task<IActionResult> UpdateMedico(int id, MedicoDTORequest request)
        {
            var response = await _repository.UpdateMedicoAsync(id, request);


            return response.Success ? Ok(response) : NotFound(response);

        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> DeleteMedico(int id)
        {
            var response = await _repository.DeleteMedicoAsync(id);

            return response.Success ? Ok(response) : NotFound(response);

        }
        [HttpPatch("{id:int}")]
        public async Task<IActionResult> ReactivarMedico(int id)
        {
            var response = await _repository.ReactivarMedicoAsync(id);

            return response.Success ? Ok(response) : NotFound(response);

        }

        [HttpGet("ListarEliminados")]
        public async Task<IActionResult> GetDeleteMedicoList()
        {
            var response = await _repository.GetDeleteMedicoListAsync();

            return Ok(response);

        }

    }
}
