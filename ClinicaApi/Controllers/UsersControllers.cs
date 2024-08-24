using Clinica.Models.Request;
using Clinica.Models.Response;
using Clinica.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace ClinicaApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersControllers : ControllerBase
    {
        private readonly IUserServices _services;

        public UsersControllers(IUserServices services)
        {
            _services = services;
        }

        [HttpPost("Login")]
        public async Task<IActionResult> Login(LogingDTORequest request)
        {
            var response = await _services.LoginAsync(request);

            return response.Success ? Ok(response) : Unauthorized(response);

        }
        [HttpPost("Register")]
        public async Task<IActionResult> Register(RegisterDTORequest request)
        {
            var response = await _services.RegisterAsync(request);
            return response.Success ? Ok(response) : BadRequest(response) ;
        }
    }
}
