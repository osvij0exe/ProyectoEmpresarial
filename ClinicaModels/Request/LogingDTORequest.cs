using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clinica.Models.Request
{
    public class LogingDTORequest
    {
        [Required]
        public string Usuario { get; set; } = default!;
        [Required]
        public string Password { get; set; } = default!;
    }
}
