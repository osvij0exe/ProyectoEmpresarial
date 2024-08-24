using Clinica.Models.Request;
using Clinica.Models.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clinica.Services.Interfaces
{
    public interface IMedicoServices
    {
        Task<PaginationResponse<MedicoDTOResponse>> ListAsync(string? filtroNombres, string? filtroApellidos, int page, int rows);
        Task<BaseResponseGeneric<MedicoDTOResponse>> FindByIdAsync(int id);
        Task<BaseResponse> AddAsync(MedicoDTORequest medicoRequest);
        Task<BaseResponse> UpdateAsync(int id, MedicoDTORequest medicoRequest);
        Task<BaseResponse> DeleteAsync(int id);
        Task<BaseResponseGeneric<ICollection<MedicoDTOResponse>>> ListarEliminadosAsync();
        Task<BaseResponse> ReactivarAsync(int id);

    }
}
