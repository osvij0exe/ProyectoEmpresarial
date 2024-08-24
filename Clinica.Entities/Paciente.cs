using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clinica.Entities
{
    public class Paciente: EntityBase
    {
        public string Nombres { get; set; } = default!;
        public string Apellidos { get; set; } = default!;
        public DateTime FechaNacimiento { get; set; }
        public string Email { get; set; } = default!;
        public string? Telefono { get; set; }
        public string Ciudad { get; set; } = default!;
        public string Provincia { get; set; } = default!;
        public int Edad { get; set; }
        public int TensionArterial { get; set; } = default!;
        public int FrecuenciaCardiaca { get; set; } = default!;
        public int FactorReumatoideo { get; set; } = default!;
        public float Temperatura { get; set; } = default!;
        public float Peso { get; set; } = default!;
        public float Talla { get; set; } = default!;
        public float IMC { get; set; } = default!;
        public string Alergias { get; set; } = default!;
        public List<Recetario> Recestas { get; set; } = default!;

    }




}
