using Clinica.Entities;
using Clinica.Models.Response;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clinica.ViewModels
{
    public class ConsultaViewModel
    {
        public string? FechaCita { get; set; }
        [Display(Name = "Nombre Medico")]
        public string? NombreMedico { get; set; }
        [Display(Name = "Apellido Medico")]
        public string? ApellidoMedico { get; set; }
        [Display(Name = "Nombre Paciente")]
        public string? NombrePaciente { get; set; }
        [Display(Name = "Apellido Paciente")]
        public string? ApellidoPaciente { get; set; }
        public ICollection<EspecialidadDTOResponse> Especialidades { get; set; } = default!;

        [Display(Name = "Esplecialidad")]
        public int? EspecialidadSeleccionada { get; set; } // para que sea percistente
        [Display(Name = "Situacion")]
        public int? SituacionSeleccionada { get; set; }
        public ICollection<SituacionConsultaModel> SituacionConsulta { get; set; } = new List<SituacionConsultaModel>()
        {
            new() { Codigo = 0, Nombre = "Consulta Finalizada"},
            new() { Codigo = 1, Nombre = "Consulta Cancelada"},
            new() { Codigo = 2, Nombre = "Consulta Pendiente"}

        };

        public int Rows { get; set; }
        public int Page { get; set; }


        public ICollection<ConsultaDTOResponse>? Consultas { get; set; }


    }

}
