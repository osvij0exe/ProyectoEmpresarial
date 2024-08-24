using Clinica.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clinica.Models.Request
{
    public class MedicoDTORequest
    {
        [Required(ErrorMessage = "El campo {0} es requerido")]
        public string Nombres { get; set; } = default!;
        [Required(ErrorMessage = "El campo {0} es requerido")]
        public string Apellidos { get; set; } = default!;
        [Required(ErrorMessage = "El campo {0} es requerido")]
        public int EspecialidadId { get; set; }
        public int CedulaProfecional { get; set; }
        public string Genero { get; set; } = default!;
        public string Correo { get; set; } = default!;
        public string Telefono { get; set; } = default!;
    }
}
