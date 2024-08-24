using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clinica.Entities
{
    public class Medico: EntityBase
    {
        public string Nombres { get; set; } = default!;
        public string Apellidos { get; set; } = default!;
        public int EspecialidadId { get; set; }
        public int CedulaProfecional { get; set; }
        public string Genero { get; set; } = default!;
        public string Correo { get; set; } = default!;
        public string Telefono { get; set; } = default!;
        public Especialidad Especialidad { get; set; } = default!; 
        public List<Consulta> Consultas { get; set; } = default!;
        public List<Recetario> Recestas { get; set; } = default!;


    }
}
