using Clinica.Models.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clinica.Models.Request
{
    public class BusquedaConsultaRequest : RequestBase
    {
        public string? FiltroFechaCita { get; set; }
        public string? NombreMedico { get; set; }
        public string? ApellidoMedico { get; set; }
        public string? NombrePaciente { get; set; }
        public string? ApellidoPaciente { get; set; }
        public int? EspecialidadId { get; set; }
        public int? SituacionConsulta { get; set; }

    }
}
