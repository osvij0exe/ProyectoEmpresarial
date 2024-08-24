using Clinica.Models;
using Clinica.Models.Request;
using Clinica.Models.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clinica.Services.Interfaces
{
    public interface IUserServices
    {
        Task<LogingDTOResponse> LoginAsync(LogingDTORequest requqest);

        Task<BaseResponse> RegisterAsync(RegisterDTORequest request);

    }
}
