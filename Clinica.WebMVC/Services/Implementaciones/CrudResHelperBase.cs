using Clinica.Models.Response;
using Clinica.WebMVC.Services.Interfaces;

namespace Clinica.WebMVC.Services.Implementaciones
{
    public class CrudResHelperBase<TRequest, TResponse> : RestBase, ICrudRestHelper<TRequest, TResponse>
        where TRequest : class
        where TResponse : class
    {
        public CrudResHelperBase(string baseUrl, HttpClient httpClient) 
            : base(baseUrl, httpClient)
        {
        }

        public async Task<PaginationResponse<TResponse>> ListAsync(string? filtro, int page = 1, int rows = 5)
        {
            var response = await HttpClient.GetFromJsonAsync<PaginationResponse<TResponse>>($"{BaseUrl}/{filtro}");
            if(response!.Success)
            {
                return response;
            }
            throw new InvalidOperationException(response.ErrorMessage);
        }
        public async Task<ICollection<TResponse>> ListAsync()
        {
            var response = await HttpClient.GetFromJsonAsync<PaginationResponse<TResponse>>($"{BaseUrl}");
            if(response!.Success )
            {
                return response.Data!;
            }
            throw new InvalidOperationException(response.ErrorMessage);
        }
        public async Task<TResponse> FindById(int id)
        {
            var response = await HttpClient.GetFromJsonAsync<BaseResponseGeneric<TResponse>>($"{BaseUrl}/{id}");
            if(response!.Success) 
            {
                return response.Data!;
            }
            throw new InvalidOperationException(response.ErrorMessage);
        }

        public async Task CreateAsync(TRequest request)
        {
            var response = await HttpClient.PostAsJsonAsync(BaseUrl, request);
            if(response.IsSuccessStatusCode)
            {
                var resultado = await response.Content.ReadFromJsonAsync<BaseResponse>();
                if(resultado!.Success == false)
                    throw new InvalidOperationException(resultado.ErrorMessage);
            }
            else
            {
                throw new InvalidOperationException(response.ReasonPhrase);
            }
        }
        public async Task UpdateAsync(int id, TRequest request)
        {
            var response = await HttpClient.PutAsJsonAsync($"{BaseUrl}/{id}", request);
            if (response.IsSuccessStatusCode)
            {
                var resultado = await response.Content.ReadFromJsonAsync<BaseResponse>();
                if (resultado!.Success == false)
                    throw new InvalidOperationException(resultado.ErrorMessage);
            }
            else
            {
                throw new InvalidOperationException(response.ReasonPhrase);
            }
        }

        public async Task DeleteAsync(int id)
        {
            var response = await HttpClient.DeleteAsync($"{BaseUrl}/{id}");
            if (response.IsSuccessStatusCode)
            {
                var resultado = await response.Content.ReadFromJsonAsync<BaseResponse>();
                if (resultado!.Success == false)
                    throw new InvalidOperationException(resultado.ErrorMessage);
            }
            else
            {
                throw new InvalidOperationException(response.ReasonPhrase);
            }
        }


    }
}
