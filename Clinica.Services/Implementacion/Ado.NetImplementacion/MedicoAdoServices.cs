using Clinica.Entities;
using Clinica.Models.Response;
using Clinica.Repositories.Implementacion.AdoRepository.AdpHelpres;
using Clinica.Repositories.Interfaces.IAdoRepository.IAdoPatronRepository;
using Clinica.Services.Interfaces.Ado.NetInterfaces;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clinica.Services.Implementacion.Ado.NetImplementacion
{
    public class MedicoAdoServices : IMedicoAdoServices
    {
        private readonly IAdoMedicoRepositoryPatron _repository;
        private readonly ILogger<MedicoAdoServices> _logger;
        private readonly GetDataReaderValues _readerValues;

        public MedicoAdoServices(IAdoMedicoRepositoryPatron repository,
            ILogger<MedicoAdoServices> logger,
            GetDataReaderValues readerValues)
        {
            _repository = repository;
            _logger = logger;
            _readerValues = readerValues;
        }

        //public async Task<List<MedicoDTOResponse>> ListAsync(string Nombres,
        //    SqlDataReader reader,
        //    SqlCommand command)
        //{
        //    //var response = new BaseResponseGeneric<MedicoDTOResponse>();

        //    try
        //    {
        //        command.Parameters.AddWithValue("@Nombres", Nombres is null ? "" : Nombres);


        //        if (reader is not null)
        //        {
        //            var medico = new List<MedicoDTOResponse>();
        //            int IdPosicion = reader.GetOrdinal("Id");

        //            while (await reader.ReadAsync())
        //            {
        //                medico.Add(new MedicoDTOResponse()
        //                {
        //                    Id = reader.IsDBNull(IdPosicion) ? 0 : reader.GetInt32(IdPosicion)
        //                });
        //            }
        //            //var tupla = await _repository.GetAdoMedicos(
        //            //    predicate: medico,
        //            //    spName: "uspObtenerMedicos",
        //            //    command: command
        //            //    );
        //            //response.Data = tupla;
        //        }
        //    }
        //    catch (Exception ex)
        //    {

        //        throw;
        //    }
        //}

        public Task<List<MedicoDTOResponse>> ListAsync(SqlDataReader reader, string spName, SqlCommand command)
        {
            throw new NotImplementedException();
        }
    }
}
