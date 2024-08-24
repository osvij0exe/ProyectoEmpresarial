using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clinica.Entities
{
    public class Consulta : EntityBase
    {
        public DateTime FechaCita { get; set; }
        public DateTime HoraCita { get; set; }
        public SituacionConsulta Situacion { get; set; }
        public int MedicoId { get; set; }
        public Medico Medico { get; set; } = default!;
        public int PacienteId { get; set; }
        public Paciente Paciente { get; set; } = default!;
        public int EspecialidadId { get; set; }
        public Especialidad Especialidad { get; set; } = default!;

    }
}
