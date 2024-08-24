using Clinica.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clinica.Models.Response.PacienteResponse
{
    public class PacienteAdoDTOResponse : EntityBase
    {
        public string Nombres { get; set; } = default!;
        public string Apellidos { get; set; } = default!;
        public string Edad { get; set; } = default!;
        public string TensionArterial { get; set; } = default!;
        public string FrecuenciaCardiaca { get; set; } = default!;
        public string FactorReumatoideo { get; set; } = default!;
        public string Temperatura { get; set; } = default!;
        public string Peso { get; set; } = default!;
        public string Talla { get; set; } = default!;
        public string IMC { get; set; } = default!;
        public string Alergias { get; set; } = default!;
    }
}
