using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clinica.Models.Response
{
    public class BaseResponse
    {
        public bool Success { get; set; }
        public string? ErrorMessage { get; set; }
    }

    public class BaseResponseGeneric<T> : BaseResponse
    {
        public T? Data { get; set; }
    }

    public class PaginationResponse<T> : BaseResponse
    {
        public ICollection<T> Data { get; set; } = default!;
        public int TotalPaginas { get; set; }
    }


}
