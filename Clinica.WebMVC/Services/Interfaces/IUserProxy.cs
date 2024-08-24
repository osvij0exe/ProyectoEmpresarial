using Clinica.Models;
using Clinica.Models.Request;
using Clinica.Models.Response;

namespace Clinica.WebMVC.Services.Interfaces
{
    public interface IUserProxy
    {
        Task<LogingDTOResponse> LoginAsync(LogingDTORequest request);

        Task<BaseResponse> RegisterAsync(RegisterDTORequest request);

    }
}
