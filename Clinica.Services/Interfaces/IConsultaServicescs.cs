using Clinica.Models.Request;
using Clinica.Models.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clinica.Services.Interfaces
{
    public interface IConsultaServicescs
    {
        Task<PaginationResponse<ConsultaDTOResponse>> ListAsync(string? filtroFechaCita,string? NombreMedico, string? ApellidoMedico,string? NombrePaciente, string? ApellidoPaciente, int? especialidadId,int? situacionConsulta, int page, int rows);
        Task<BaseResponseGeneric<ConsultaDTOResponse>> FindByIdAsync(int id);
        Task<BaseResponse> AddAsync(ConsultaDTORequest consultaRequest);
        Task<BaseResponse> UpdateAsync(int id, ConsultaDTORequest consultaRequest);
        Task<BaseResponse> DeleteAsync(int id);
        Task<BaseResponseGeneric<ICollection<ConsultaDTOResponse>>> ListarEliminadosAsync();
        Task<BaseResponse> ReactivarAsync(int id);

    }
}
