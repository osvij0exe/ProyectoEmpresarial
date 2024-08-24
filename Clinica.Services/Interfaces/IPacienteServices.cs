using Clinica.Models.Request;
using Clinica.Models.Response;
using Clinica.Models.Response.PacienteResponse;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clinica.Services.Interfaces
{
    public interface IPacienteServices
    {
        Task<PaginationResponse<PacienteDTOResponse>> ListAsync(string? filtroNombres, string? filtroApellidos, int page, int rows);
        Task<BaseResponseGeneric<PacienteDTOResponse>> FindByIdAsync(int id);
        Task<BaseResponse> AddAsync(PacienteDTORequest pacienteRequest);
        Task<BaseResponse> UpdateAsync(int id, PacienteDTORequest pacienteRequest);
        Task<BaseResponse> DeleteAsync(int id);
        Task<BaseResponseGeneric<ICollection<PacienteDTOResponse>>> ListarEliminadosAsync();
        Task<BaseResponse> ReactivarAsync(int id);
        Task<BaseResponse> PatchAsync(int id, PacienteDTOPatch pacienteRequest);

    }
}
