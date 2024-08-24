using Clinica.ViewModels;
using Clinica.WebMVC.Services.Interfaces;

namespace Clinica.WebMVC.Services.Implementaciones
{
    public class UBigeoProxy : IUbigeoProxy
    {
        private readonly HttpClient _httpClient;

        public string UrlBase { get; set; } = default!;

        public UBigeoProxy()
        {
            _httpClient = new HttpClient();
        }

        public async Task<ICollection<CiudadModel>> ListarCiudades()
        {
            var response = await _httpClient.GetFromJsonAsync<ICollection<CiudadModel>>($"{UrlBase}/data/Ciudades.json");

            return response ?? new List<CiudadModel>();

        }
        
        public async Task<ICollection<DelegacionModel>> ListarDelegaciones(string CodigoCiudad)
        {
            var response = await _httpClient.GetFromJsonAsync<ICollection<DelegacionModel>>($"{UrlBase}/data/Delegaciones.json");

            var query = from filas in response
                        where filas.CodigoCiudad == CodigoCiudad
                        select filas;
            return query.ToList();

        }
    }
}
