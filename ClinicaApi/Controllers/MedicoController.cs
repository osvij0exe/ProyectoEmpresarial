using Clinica.Common;
using Clinica.Models.Request;
using Clinica.Models.Response;
using Clinica.Repositories.Interfaces.IAdoRepository;
using Clinica.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ClinicaApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    //[Authorize(Roles = Constantes.RolAdmin)]
    public class MedicoController : ControllerBase
    {
        private readonly IMedicoServices _services;

        public MedicoController(IMedicoServices services)
        {
            _services = services;
        }

        [HttpPost]
        public async Task<IActionResult> Post(MedicoDTORequest medicoRequest)
        {
            var response = await _services.AddAsync(medicoRequest);

            return response.Success ? Ok(response) : BadRequest(response);
        }



        [HttpGet]
        public async Task<IActionResult> Get(string? Nombres, string? Apellidos,int page = 1,int rows = 5 )
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
        public async Task<IActionResult> Put(int id, MedicoDTORequest medicoRequest)
        {
            var response = await _services.UpdateAsync(id, medicoRequest);

            return response.Success ? Ok(response) : NotFound(response);

        }
        [HttpDelete("({id:int}")]
        public async Task<IActionResult> Delete(int id)
        {
            var response = await  _services.DeleteAsync(id);

            return response.Success ? Ok(response) : NotFound(response);
        }

        [HttpPatch]
        public async Task<IActionResult> PatachReactivar(int id)
        {
            var response = await _services.ReactivarAsync(id);

            return response.Success ? Ok(response) : NotFound(response);
        }

    }
}
