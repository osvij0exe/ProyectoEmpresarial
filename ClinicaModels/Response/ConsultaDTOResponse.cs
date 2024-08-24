using Clinica.Entities;
using Clinica.Models.Response.MedicoResponse;
using Clinica.Models.Response.PacienteResponse;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clinica.Models.Response
{
    public class ConsultaDTOResponse
    {
        public string FechaCita { get; set; } = default!;
        public string HoraCita { get; set; } = default!;
        public string Situacion { get; set; } = default!;
        public MedicoConsultaDTO Medico { get; set; } = default!;
        public PacienteConsultaDTO Paciente { get; set; } = default!;
        public string Especialidad { get; set; } = default!;

    }
}
