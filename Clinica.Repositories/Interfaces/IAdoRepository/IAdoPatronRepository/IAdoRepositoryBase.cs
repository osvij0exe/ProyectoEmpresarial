using Clinica.Entities;
using Clinica.Models.Response.MedicoResponse;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Clinica.Repositories.Interfaces.IAdoRepository.AdoPatronRepository
{
    public interface IAdoRepositoryBase<TEntity> where TEntity : EntityBase
    {

        Task<TInfo> GetAdoMedicos<TInfo>(
            Func<SqlDataReader, TInfo> predicate,
            string spName,
            SqlCommand command);

    }
}
