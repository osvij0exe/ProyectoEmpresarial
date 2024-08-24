using Clinica.Models.Request;
using Clinica.Services.Implementacion.GenerarPDF;
using Clinica.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace ClinicaApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class RecetariosController : ControllerBase
    {
        private readonly IRecetarioServices _services;

        public RecetariosController(IRecetarioServices services)
        {
            _services = services;
        }

        [HttpPost]
        public async Task<IActionResult> Post(RecetarioDTORequest recetarioRequest)
        {
            var response = await _services.AddAsync(recetarioRequest);

            return response.Success ? Ok(response) : BadRequest(response);  
        }


        [HttpPut("{id:int}")]
        public async Task<IActionResult> Put(int id, RecetarioDTORequest recetarioRequest)
        {
            var response = await _services.UpdateAsync(id, recetarioRequest);

            return response.Success ? Ok(response) : NotFound(response);
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetById(int id)
        {
            var response = await _services.FindByIdAsync(id);

            return response.Success ? Ok(response) : NotFound(response);   
        }

        //[HttpGet("{id:int}")]
        //public async Task<IActionResult> GenerarPDF(int id)
        //{

        //}

    }
}
