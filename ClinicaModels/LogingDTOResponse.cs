using Clinica.Models.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clinica.Models
{
    public class LogingDTOResponse: BaseResponse
    {
        public string Nombres { get; set; } = default!;
        public string Apellidos { get; set; } = default!;
        public string Token { get; set; } = default!;
    }
}
