using Clinica.Models;
using Clinica.Models.Request;
using Clinica.Models.Response;
using Clinica.WebMVC.Services.Interfaces;

namespace Clinica.WebMVC.Services.Implementaciones
{
    public class Userproxy : RestBase, IUserProxy
    {
        public Userproxy(HttpClient httpClient)
            : base("api/UsersControllers", httpClient)
        {

        }

        public async Task<LogingDTOResponse> LoginAsync(LogingDTORequest request)
        {
            return await SendAsync<LogingDTORequest, LogingDTOResponse>(request, HttpMethod.Post, "Login");
        }

        public async Task<BaseResponse> RegisterAsync(RegisterDTORequest request)
        {
            return await SendAsync<RegisterDTORequest, BaseResponse>(request, HttpMethod.Post, "Register");
        }
    }
}
