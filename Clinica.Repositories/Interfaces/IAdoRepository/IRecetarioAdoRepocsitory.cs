using Clinica.Models.Request;
using Clinica.Models.Response;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clinica.Repositories.Interfaces.IAdoRepository
{
    public interface IRecetarioAdoRepocsitory
    {

        Task<BaseResponse> AddAdoRecetarioAsync(RecetarioDTORequest request);

        Task<BaseResponseGeneric<RecetarioAdoDTOResponse>> FindRecetabyId(int id);
        Task<BaseResponse> DeletRecetaAdoAsync(int id);

        Task<BaseResponse> ReactivarRecetaAdoAsync(int id);
        Task<BaseResponseGeneric<RecetarioAdoDTOResponse>> ListDeleteRecetaAdoAync();
        void ComposeContent(IContainer container, int id);




    }
}
