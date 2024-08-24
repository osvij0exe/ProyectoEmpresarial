using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clinica.Models.Response
{
    public class RegisterDTORequest
    {
        [Required]
        public string Usuario { get; set; } = default!;
        [Required]
        public string Nombres { get; set; } = default!;
        [Required]
        public string Apellidos { get; set; } = default!;
        [EmailAddress]
        public string Email { get; set; } = default!;
        [Required]
        public string Telefono { get; set; } = default!;
        [Required]
        public string Password { get; set; } = default!;
        [Compare(nameof(Password))]
        public string ConfirmPassword { get; set; } = default!;
        [Display(Name = "Ciudad")]
        public string CodigoCiudad { get; set; } = default!;
        [Display(Name = "Delegación")]
        public string CodigoDelegacion { get; set; } = default!;

    }
}
