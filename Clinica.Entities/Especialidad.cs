using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clinica.Entities
{
    public class Especialidad : EntityBase
    {
        public string NombreEspecialidad { get; set; } = default!;
        public List<Consulta> Consultas { get; set; } = default!;

        public override string ToString()
        {
            return NombreEspecialidad;
        }


    }
}
