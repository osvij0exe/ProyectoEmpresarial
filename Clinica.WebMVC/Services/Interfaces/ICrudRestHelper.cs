using Clinica.Models.Response;

namespace Clinica.WebMVC.Services.Interfaces
{
    public interface ICrudRestHelper<in TRequest, TResponse>
        where TRequest : class
        where TResponse : class
    {

        Task<PaginationResponse<TResponse>> ListAsync(string? filtro, int page = 1, int rows = 5);
        Task<ICollection<TResponse>> ListAsync();
        Task<TResponse> FindById(int id);
        Task CreateAsync(TRequest request);
        Task UpdateAsync(int id, TRequest request);
        Task DeleteAsync(int id);

    }
}
