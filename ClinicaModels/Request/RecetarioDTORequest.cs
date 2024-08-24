using Clinica.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clinica.Models.Request
{
    public class RecetarioDTORequest
    {
        [Required]
        public string Prescripcion { get; set; } = default!;
        [Required]
        public int PacienteId { get; set; } = default!;
        [Required]
        public int MedicoId { get; set; } = default!;
    }
}
