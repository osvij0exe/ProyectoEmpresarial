using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clinica.Models.Response.PacienteResponse
{
    public class PacienteDTOPatch
    {
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
