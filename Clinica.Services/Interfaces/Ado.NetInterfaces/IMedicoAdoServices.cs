using Clinica.Models.Response;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clinica.Services.Interfaces.Ado.NetInterfaces
{
    public interface IMedicoAdoServices
    {
        Task<List<MedicoDTOResponse>> ListAsync(SqlDataReader reader,
            string spName,
            SqlCommand command);
    }
}
