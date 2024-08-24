using Clinica.Entities;
using Clinica.Models.Response.MedicoResponse;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clinica.Models.Response
{
    public class RecetarioDTOResponse :EntityBase
    {

        public string Prescripcion { get; set; } = default!;
        public PacienteDTOResponse Paciente { get; set; } = default!;
        public MedicoDTOResponse Medico { get; set; } = default!;
    }
}
