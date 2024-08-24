using Clinica.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clinica.Models.Request
{
    public class PacienteDTORequest
    {
        [Required(ErrorMessage =" el campo {0} es requerido")]
        public string Nombres { get; set; } = default!;
        [Required(ErrorMessage =" el campo {0} es requerido")]
        public string Apellidos { get; set; } = default!;
        [Required(ErrorMessage =" el campo {0} es requerido")]
        public DateTime FechaNacimiento { get; set; }
        [Required(ErrorMessage =" el campo {0} es requerido")]
        public string Email { get; set; } = default!;
        [Required(ErrorMessage =" el campo {0} es requerido")]
        public string? Telefono { get; set; }
        [Required(ErrorMessage =" el campo {0} es requerido")]
        public string Ciudad { get; set; } = default!;
        [Required(ErrorMessage =" el campo {0} es requerido")]
        public string Provincia { get; set; } = default!;
        public int Edad { get; set; }
        public int TensionArterial { get; set; } = default!;
        public int FrecuenciaCardiaca { get; set; } = default!;
        public int FactorReumatoideo { get; set; } = default!;
        public double Temperatura { get; set; } = default!;
        public double Peso { get; set; } = default!;
        public double Talla { get; set; } = default!;
        public double IMC { get; set; } = default!;
        public string Alergias { get; set; } = default!;
    }
}
