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
    public class RecetarioAdoDTOResponse :EntityBase
    {
        public string Prescripcion { get; set; } = default!;
        public PacienteAdoDTOResponse Paciente { get; set; } = default!;
        public MedicoAdoDTOResponse Medico { get; set; } = default!;
    }
}
