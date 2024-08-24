using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clinica.Models.Request
{
    public class EspecialidadDTORequest
    {
        public string Id { get; set; } = default!;
        public string NombreEspecialidad { get; set; } = default!;
    }
}
