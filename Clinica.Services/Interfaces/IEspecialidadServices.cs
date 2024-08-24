using Clinica.Models.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clinica.Services.Interfaces
{
    public interface IEspecialidadServices
    {
        Task<PaginationResponse<EspecialidadDTOResponse>> ListAsync(string? filtroNombresEspecialidad, int page, int rows);
        Task<BaseResponseGeneric<ICollection<EspecialidadDTOResponse>>> ListAsync();
        Task<BaseResponseGeneric<EspecialidadDTOResponse>> FindByIdAsync(int id);

    }
}
