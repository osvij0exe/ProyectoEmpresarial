using Azure;
using Clinica.Models.Request;
using Clinica.Models.Response.PacienteResponse;
using Clinica.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace ClinicaApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PacienteController : ControllerBase
    {
        private readonly IPacienteServices _services;

        public PacienteController(IPacienteServices services)
        {
            _services = services;
        }

        [HttpPost]
        public async Task<IActionResult> Post(PacienteDTORequest pacineteRequest)
        {
            var response = await _services.AddAsync(pacineteRequest);

            return response.Success ? Ok(response) : BadRequest(response);

        }
        [HttpGet]
        public async Task<IActionResult> Get(string? Nombres, string? Apellidos, int page = 1 , int rows = 5)
        {
            var response = await _services.ListAsync(Nombres, Apellidos, page, rows);

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
        public async Task<IActionResult> Put(int id, PacienteDTORequest pacineteRequest)
        {
            var response = await _services.UpdateAsync(id, pacineteRequest);

            return response.Success ? Ok(response) : NotFound(response);
        }
        [HttpPatch("PartialUpdate/{id:int}")]
        public async Task<IActionResult> PatchPropiedades(int id, PacienteDTOPatch patchDocument)
        {
            var response = await _services.PatchAsync(id, patchDocument);

            return response.Success ? Ok(response) : BadRequest(response);  
        }


        [HttpDelete("id:int")]
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
