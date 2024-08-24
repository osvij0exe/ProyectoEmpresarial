using Clinica.Models.Request;
using Clinica.Models.Response;
using Clinica.Models.Response.MedicoResponse;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clinica.Repositories.Interfaces.IAdoRepository
{
    public interface IAdoMedicoRepository
    {
        Task<BaseResponseGeneric<List<MedicoAdoDTOResponse>>> GetAdoMedicosAsync(string? nombres, string? apellidos,string? cedulaProfecional, string? Especialidad);

        Task<BaseResponseGeneric<MedicoAdoDTOResponse>> FindByIdAsync(int id);

        Task<BaseResponse> AddMedicoAsync(MedicoDTORequest request);
        Task<BaseResponse> UpdateMedicoAsync(int id, MedicoDTORequest request);

        Task<BaseResponse> DeleteMedicoAsync(int id);
        Task<BaseResponse> ReactivarMedicoAsync(int id);
        Task<BaseResponseGeneric<List<MedicoAdoDTOResponse>>> GetDeleteMedicoListAsync();


    }
}
