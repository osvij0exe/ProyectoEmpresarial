using Clinica.AccesoADatos.JsonSettings;
using Clinica.Entities;
using Clinica.Repositories.Interfaces.IAdoRepository.AdoPatronRepository;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Clinica.Repositories.Implementacion.AdoRepository.AdoRepositoryPatron
{
    public class AdoRepositoryBase<TEntity> : IAdoRepositoryBase<TEntity> where TEntity : EntityBase
    {
        private readonly ConnectionSetting _connection;

        public AdoRepositoryBase(IOptions<ConnectionSetting> connection)
        {
            _connection = connection.Value;
        }
        public async Task<TInfo> GetAdoMedicos<TInfo>(
            Func<SqlDataReader, TInfo> predicate,
            string spName,
            SqlCommand command)
        {


            using (var conncetionDB = new SqlConnection(_connection.ClinicaDB))
            {
                using (var sqlCommand = conncetionDB.CreateCommand())
                {
                    conncetionDB.Open();
                    SqlCommand cmd = new SqlCommand(spName, conncetionDB);
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    if(command is not null)
                    {
                        cmd.Parameters.Add(command);
                    }


                    //sqlCommand.CommandType = System.Data.CommandType.StoredProcedure;
                    //sqlCommand.CommandText = spName;
                    //if (parameters != null)
                    //{
                    //    sqlCommand.Parameters.AddRange(parameters);
                    //}
                    //conncetionDB.Open();

                    using (var reader = await cmd.ExecuteReaderAsync(CommandBehavior.SingleResult))
                    {
                        TInfo elements;
                        try
                        {
                            elements = predicate(reader);
                        }
                        catch (Exception)
                        {

                            throw;
                        }
                        finally
                        {
                            while (await reader.NextResultAsync()) ;
                        }
                        return elements;
                    }
                }
            }
        }
    }
}
