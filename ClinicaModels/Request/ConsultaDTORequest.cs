using Clinica.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clinica.Models.Request
{
    public class ConsultaDTORequest
    {
        [Required(ErrorMessage = "El campo {0} es requerido")]
        [Display(Name ="Fecha Cita")]
        public DateTime FechaCita { get; set; }
        [Required(ErrorMessage = "El campo {0} es requerido")]
        [Display(Name = "Hora Cita")]
        public DateTime HoraCita { get; set; }
        [Required(ErrorMessage = "El campo {0} es requerido")]
        [Display(Name = "Situacion")]
        public SituacionConsulta Situacion { get; set; }
        [Required(ErrorMessage = "El campo {0} es requerido")]
        [Display(Name = "Medico")]
        public int MedicoId { get; set; }
        [Required(ErrorMessage = "El campo {0} es requerido")]
        [Display(Name = "Paciente")]
        public int PacienteId { get; set; }
        [Required(ErrorMessage = "El campo {0} es requerido")]
        [Display(Name = "Especialidad")]
        public int EspecialidadId { get; set; }

    }
}
