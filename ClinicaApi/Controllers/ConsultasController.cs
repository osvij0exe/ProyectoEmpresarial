using Clinica.Models.Request;
using Clinica.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace ClinicaApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ConsultasController : ControllerBase 
    {
        private readonly IConsultaServicescs _services;

        public ConsultasController(IConsultaServicescs services)
        {
            _services = services;
        }
        [HttpPost]
        public async Task<IActionResult> Post(ConsultaDTORequest consultaRequest)
        {
            var response = await _services.AddAsync(consultaRequest);

            return response.Success ? Ok(response) : BadRequest(response);
        }

        [HttpGet]
        public async Task<IActionResult> Get(string? filtroFechaCita,
            string? NombreMedico,
            string? ApellidoMedico,
            string? NombrePaciente,
            string? ApellidoPaciente,
            int? especialidadId,
            int? situacionConsulta,
            int page = 1,
            int rows = 5)
        //[HttpGet]
        //public async Task<IActionResult> Get([FromQuery] BusquedaConsultaRequest request) // mas legible
        {
            var response = await _services.ListAsync(filtroFechaCita,
                NombreMedico,
                ApellidoMedico,
                NombrePaciente,
                ApellidoPaciente,
                especialidadId,
                situacionConsulta,
                page, rows);

            return Ok(response);
        }
        [HttpGet("{id:int}")]
        public async Task<IActionResult> Get(int id)
        {
            var response = await _services.FindByIdAsync(id);

            return response.Success ? Ok(response) : NotFound(response);

        }
        [HttpGet("ListarEliminados")]
        public async Task<IActionResult> ListarEliminados()
        {
            var response = await _services.ListarEliminadosAsync();


            return Ok(response);
        }

        [HttpPut("{id:int}")]
        public async Task<IActionResult> Put(int id, ConsultaDTORequest consultaDTORequest)
        {
            var response = await _services.UpdateAsync(id, consultaDTORequest);

            return response.Success ? Ok(response) : NotFound(response);
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete(int id)
        {
            var response = await _services.DeleteAsync(id);

            return response.Success ? Ok(response) : NotFound(response);

        }
        [HttpPatch("{id:int}")]
        public async Task<IActionResult> PatchReactivar(int id)
        {
            var response = await _services.ReactivarAsync(id);

            return response.Success ? Ok(response) : NotFound(response);        
        }
    }
}
