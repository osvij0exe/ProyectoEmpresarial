using Clinica.Models.Request;
using Clinica.Models.Response;
using Clinica.WebMVC.Services.Interfaces;

namespace Clinica.WebMVC.Services.Implementaciones
{
    public class ConsultaProxy : CrudResHelperBase<ConsultaDTORequest, ConsultaDTOResponse>, IConsultaProxy
    {
        public ConsultaProxy( HttpClient httpClient)
            : base("api/Consultas", httpClient)
        {
        }

        public async Task<PaginationResponse<ConsultaDTOResponse>> ListAsync(BusquedaConsultaRequest request)
        {
            var response = await HttpClient.GetFromJsonAsync<PaginationResponse<ConsultaDTOResponse>
                >($"{BaseUrl}?NombreMedico={request.NombreMedico}&ApellidoMedico={request.ApellidoMedico}" +
                $"&NombrePaciente={request.NombrePaciente}&ApellidoPaciente{request.ApellidoPaciente}&FiltroFechaCita={request.FiltroFechaCita}" +
                $"&EspecialidadId={request.EspecialidadId}&SituacionConsulta={request.SituacionConsulta}" +
                $"&page={request.Page}&rows={request.Rows}");

            if (response is { Success: true })
            {
                return response;
            }
            return await Task.FromResult(new PaginationResponse<ConsultaDTOResponse>());
        }
    }
}
