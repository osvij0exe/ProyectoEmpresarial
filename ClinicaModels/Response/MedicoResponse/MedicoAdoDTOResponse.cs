using Clinica.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clinica.Models.Response.MedicoResponse
{
    public class MedicoAdoDTOResponse : EntityBase
    {
        public string Nombres { get; set; } = default!;
        public string Apellidos { get; set; } = default!;
        public EspecialidadDTOResponse RefEspecialidad { get; set; } = default!;
        public string CedulaProfecional { get; set; } = default!;
        public string Genero { get; set; } = default!;
        public string Correo { get; set; } = default!;
        public string Telefono { get; set; } = default!;
    }
}
