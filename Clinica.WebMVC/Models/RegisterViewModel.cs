using Clinica.Models.Response;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Clinica.WebMVC.Models
{
    public class RegisterViewModel
    {
        public RegisterDTORequest Input { get; set; } = default!;

        public List<SelectListItem> ListaCiudades { get; set; } = default!;
    }
}
