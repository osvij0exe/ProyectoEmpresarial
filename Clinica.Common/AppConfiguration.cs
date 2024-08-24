// desactiva la comprobacion de nulos a nivel de archivos
#nullable disable
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clinica.Common
{

    public class AppConfiguration
    {
        public Jwt Jwt { get; set; }
    }

    public class Jwt
    {
        public string SecretKey { get; set; }
        public string Audiencia { get; set; }
        public string Emisor { get; set; }
    }


}
