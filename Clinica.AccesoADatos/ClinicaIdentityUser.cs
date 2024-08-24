
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clinica.AccesoADatos
{
    public class ClinicaIdentityUser  : IdentityUser
    {
        public string Nombre { get; set; } = default!;
        public string Apellido { get; set; } = default!;
    }

}
