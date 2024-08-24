using Clinica.Models.Request;
using Clinica.Models.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clinica.ViewModels
{
    public class FormConsultaViewModel
    {
        public ConsultaDTORequest Input { get; set; } = default!;

        public ICollection<EspecialidadDTOResponse> Especialidades { get; set; } = default!;

        public SituacionConsultaModel SituacionConsulta { get; set; } = new SituacionConsultaModel()
        {
            Codigo = 2, Nombre = "Consulta Pendiente"
        };
    }
}
