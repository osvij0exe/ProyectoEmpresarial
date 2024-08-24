using Clinica.Models.Request;
using Clinica.Models.Response;

namespace Clinica.WebMVC.Services.Interfaces
{
    public interface IConsultaProxy : ICrudRestHelper<ConsultaDTORequest, ConsultaDTOResponse>
    {
        Task<PaginationResponse<ConsultaDTOResponse>> ListAsync(BusquedaConsultaRequest request);
    }
}
