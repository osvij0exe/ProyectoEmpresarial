using Clinica.Models.Request;
using Clinica.Models.Response;
using Clinica.WebMVC.Services.Interfaces;

namespace Clinica.WebMVC.Services.Implementaciones
{
    public class EspecialidadProxy : CrudResHelperBase<EspecialidadDTORequest, EspecialidadDTOResponse>, IEspecialidadProxy
    {
        public EspecialidadProxy(HttpClient httpClient) 
            : base("api/Especialidades", httpClient)
        {
        }
    }
}
