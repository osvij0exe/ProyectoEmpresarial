using Clinica.Models.Request;
using Clinica.Models.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clinica.Services.Interfaces
{
    public interface IRecetarioServices
    {
        Task<BaseResponse> AddAsync(RecetarioDTORequest recetarioRequest);

        Task<BaseResponse> UpdateAsync(int id, RecetarioDTORequest recetarioRequest);

        Task<BaseResponseGeneric<RecetarioDTOResponse>> FindByIdAsync(int id);





    }
}
