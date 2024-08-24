using Clinica.ViewModels;

namespace Clinica.WebMVC.Services.Interfaces
{
    public interface IUbigeoProxy
    {
        public string UrlBase { get; set; }

        Task<ICollection<CiudadModel>> ListarCiudades();
        Task<ICollection<DelegacionModel>> ListarDelegaciones(string CodigoCiudad);

    }
}
