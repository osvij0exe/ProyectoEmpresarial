using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clinica.Entities
{
    public class Recetario : EntityBase
    {
        public string Prescripcion { get; set; } = default!;
        public int PacienteId { get; set; } = default!;
        public Paciente Paciente { get; set; } = default!;
        public int MedicoId { get; set; } = default!;
        public Medico Medico { get; set; } = default!;  

    }
}
