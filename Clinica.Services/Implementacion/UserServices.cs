using Azure.Core;
using Clinica.AccesoADatos;
using Clinica.Common;
using Clinica.Entities;
using Clinica.Models;
using Clinica.Models.Request;
using Clinica.Models.Response;
using Clinica.Repositories.Interfaces;
using Clinica.Services.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Clinica.Services.Implementacion
{
    public class UserServices : IUserServices
    {
        private readonly UserManager<ClinicaIdentityUser> _userManager;
        private readonly ILogger<UserServices> _logger;
        private readonly IPacienteRepository _repository;
        private readonly AppConfiguration _configurtation;

        public UserServices(UserManager<ClinicaIdentityUser> userManager, 
            IOptions<AppConfiguration> options,
            ILogger<UserServices> logger,
            IPacienteRepository repository)
        {
            _userManager = userManager;
            _logger = logger;
            _repository = repository;
            _configurtation = options.Value;
        }
        public async Task<LogingDTOResponse> LoginAsync(LogingDTORequest request)
        {
            var response = new LogingDTOResponse();
            try
            {

                var identity = await _userManager.FindByNameAsync(request.Usuario);
                if(identity is null)
                {
                    throw new SecurityException("Usuario no existe");
                }
                // validar que el usario esta bloqueado por intentos
                if(await _userManager.IsLockedOutAsync(identity))
                {
                    throw new SecurityException($"Demasiados intentos fallidos para el usuario {request.Usuario}");
                }

                var result = await _userManager.CheckPasswordAsync(identity, request.Password);
                if(!result)
                {
                    response.ErrorMessage = "Clave incorrecta";

                    _logger.LogWarning("Error de autenticación para el usuario {UserName}", request.Usuario);
                    await _userManager.AccessFailedAsync(identity);// Aumenta el contador de claves erroneas

                    return response;
                }

                var roles = await _userManager.GetRolesAsync(identity);
                var fechaVencimiento = DateTime.Now.AddHours(6);

                var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name,identity.Nombre),
                    new Claim(ClaimTypes.Surname,identity.Apellido),
                    new Claim(ClaimTypes.Role, roles.First()),
                    new Claim(ClaimTypes.Expiration, fechaVencimiento.ToString("yyyy-MM-dd HH:mm:ss"))

                };
                //Creación del JWT (header/pyload/signature)
                var llaveSimetrica = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configurtation.Jwt.SecretKey));

                var Credenciales = new SigningCredentials(llaveSimetrica, SecurityAlgorithms.HmacSha256);

                var header  = new JwtHeader(Credenciales);
                var payload = new JwtPayload(
                    issuer: _configurtation.Jwt.Emisor,
                    audience: _configurtation.Jwt.Audiencia,
                    claims: claims,
                    notBefore: DateTime.Now,
                    expires: fechaVencimiento);

                var token          = new JwtSecurityToken(header, payload);
                response.Token     = new JwtSecurityTokenHandler().WriteToken(token);
                response.Nombres   = identity.Nombre;
                response.Apellidos = identity.Apellido;
                response.Success   = true;
            }
            catch(SecurityException ex)
            {
                response.ErrorMessage = ex.Message;
                _logger.LogWarning(ex, "{Message}", ex.Message);

            }
            catch (Exception ex)
            {

                response.ErrorMessage = "Error al autenticar";
                _logger.LogCritical(ex, "{ErrorMessage} {message}", response.ErrorMessage, ex.Message);
            }
            return response;
        }

        public async Task<BaseResponse> RegisterAsync(RegisterDTORequest request)
        {
            var response = new BaseResponse();
            try
            {
                var user = new ClinicaIdentityUser
                {
                    Nombre = request.Nombres,
                    UserName = request.Usuario,
                    Apellido = request.Apellidos,
                    Email = request.Email,
                    PhoneNumber = request.Telefono,
                    EmailConfirmed = true,
                };

                var result = await _userManager.CreateAsync(user, request.Password);
                if (result.Succeeded)
                {
                    //Esto me asegura que el usuario se creó correctamente
                    user = await _userManager.FindByEmailAsync(user.Email);
                    if (user is not null)
                    {
                        await _userManager.AddToRoleAsync(user, Constantes.RolPaciente);

                        // TODO: Aqui debemos registrar al paciente en la tabla de pacientes
                        var paciente = new Paciente
                        {
                            Nombres = request.Nombres,
                            Email = request.Email,
                            Telefono = request.Telefono,
                            Ciudad = request.CodigoCiudad,
                            Provincia = request.CodigoDelegacion

                        };
                        await _repository.AddAsync(paciente);


                    //TODO: Enviar Un Email

                    }
                    else
                    {
                        var sb = new StringBuilder();
                        foreach(var error in result.Errors)
                        {
                            sb.Append($"{error.Description}, ");
                        }
                        response.ErrorMessage = sb.ToString();
                        sb.Clear(); // libera la memoria
                    }

                    response.Success  = result.Succeeded;
                }
            }
            catch (Exception ex)
            {

                response.ErrorMessage = "Error al autenticar";
                _logger.LogCritical(ex, "{ErrorMessage} {message}", response.ErrorMessage, ex.Message);
            }

            return response;
        }
    }
}
