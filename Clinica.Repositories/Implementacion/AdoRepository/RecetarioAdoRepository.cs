using Clinica.AccesoADatos.JsonSettings;
using Clinica.Models.Request;
using Clinica.Models.Response;
using Clinica.Models.Response.MedicoResponse;
using Clinica.Models.Response.PacienteResponse;
using Clinica.Repositories.Interfaces.IAdoRepository;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clinica.Repositories.Implementacion.AdoRepository
{
    public class RecetarioAdoRepository : IRecetarioAdoRepocsitory
    {
        private readonly ConnectionSetting _connection;
        private readonly ILogger<RecetarioAdoRepository> _logger;

        public RecetarioAdoRepository(IOptions<ConnectionSetting> connection,
            ILogger<RecetarioAdoRepository> logger)
        {
            _connection = connection.Value;
            _logger = logger;
        }


        public async Task<BaseResponse> AddAdoRecetarioAsync(RecetarioDTORequest request)
        {
            var response = new BaseResponse();

            using (var connection = new SqlConnection(_connection.ClinicaDB))
            {
                try
                {
                    int SqlResponse = 0;
                    await connection.OpenAsync();


                    SqlCommand cmd = new SqlCommand("uspAddReceta", connection);
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@Prescripcion",request.Prescripcion);
                    cmd.Parameters.AddWithValue("@MedicoId", request.MedicoId);
                    cmd.Parameters.AddWithValue("@PacienteId", request.PacienteId);
                    SqlResponse = await cmd.ExecuteNonQueryAsync();

                    if (SqlResponse != 0) 
                    {
                        response.Success = true;   
                    }
                    else
                    {
                        throw new InvalidOperationException("No se pudo guardar la receta");
                    }

                }
                catch (Exception ex)
                {

                    response.ErrorMessage = "Error al crear Receta";
                    _logger.LogCritical(ex, "{ErrorMessage}{Message}", response.ErrorMessage, ex.Message);
                    await connection.CloseAsync();
                }
                return response;
            }


        }


        public async Task<BaseResponseGeneric<RecetarioAdoDTOResponse>> FindRecetabyId(int id)
        {
            var response = new BaseResponseGeneric<RecetarioAdoDTOResponse>();

            using ( var connectionDB = new SqlConnection(_connection.ClinicaDB))

                try
                {
                    var recetario = new RecetarioAdoDTOResponse();


                    await connectionDB.OpenAsync();

                    SqlCommand cmd = new SqlCommand("uspRecetaGetById", connectionDB);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@Id",id.Equals(0) ? "" : id);


                    using (var reader = await cmd.ExecuteReaderAsync(CommandBehavior.SingleResult))
                    {
                        if(reader is not null)
                        {
                            //recetario = new RecetarioAdoDTOResponse();
                            int IdPosicion = reader.GetOrdinal("Id");
                            int FechaCreacionPosicion = reader.GetOrdinal("FechaCreacion");
                            int PrescripcionPosicion = reader.GetOrdinal("Prescripcion");
                            int NombresPacientePosicion = reader.GetOrdinal("Nombres");
                            int ApellidosPacientePosicion = reader.GetOrdinal("Apellidos");
                            int EdadPosicion = reader.GetOrdinal("Edad");
                            int AlergiasPosicion = reader.GetOrdinal("Alergias");
                            int FactorReumatoideoPosicion = reader.GetOrdinal("FactorReumatoideo");
                            int FrecuenciaCardiaca = reader.GetOrdinal("FrecuenciaCardiaca");
                            int IMCPosicion = reader.GetOrdinal("IMC");
                            int PesoPosicion = reader.GetOrdinal("Peso");
                            int TallaPosicion = reader.GetOrdinal("Talla");
                            int TemperaturaPosicion = reader.GetOrdinal("Temperatura");
                            int TensionArterialPosicion = reader.GetOrdinal("TensionArterial");
                            int NombresMedicoPosicion = reader.GetOrdinal("NombresMedico");
                            int ApellidosMedicosPosicion = reader.GetOrdinal("ApellidosMedico");
                            int CorreoPosicion = reader.GetOrdinal("Correo");
                            int CedulaProfecionalPosicion = reader.GetOrdinal("CedulaProfecional");
                            int GeneroPosicion = reader.GetOrdinal("Genero");
                            int TelefonoPosicion = reader.GetOrdinal("Telefono");
                            int NombreEspecialidadPosicion = reader.GetOrdinal("NombreEspecialidad");

                            while(await reader.ReadAsync())
                            {
                                recetario = new RecetarioAdoDTOResponse()
                                {
                                    Id = reader.IsDBNull(IdPosicion) ? 0 : reader.GetInt32(IdPosicion),
                                    FechaCreacion = Convert.ToDateTime(reader.IsDBNull(FechaCreacionPosicion) ? (DateTime?)null : reader.GetDateTime(FechaCreacionPosicion).ToString()),
                                    Prescripcion = reader.IsDBNull(PrescripcionPosicion) ? "" : reader.GetString(PrescripcionPosicion),
                                    Paciente = new PacienteAdoDTOResponse()
                                    {
                                        Nombres = reader.IsDBNull(NombresPacientePosicion) ? "" : reader.GetString(NombresPacientePosicion),
                                        Apellidos = reader.IsDBNull(ApellidosPacientePosicion) ? "" : reader.GetString(ApellidosPacientePosicion),
                                        Edad = reader.IsDBNull(EdadPosicion) ? "" : reader.GetInt32(EdadPosicion).ToString(),
                                        Alergias = reader.IsDBNull(AlergiasPosicion) ? "" : reader.GetString(AlergiasPosicion),
                                        FactorReumatoideo = reader.IsDBNull(FactorReumatoideoPosicion) ? "" : reader.GetInt32(FactorReumatoideoPosicion).ToString(),
                                        FrecuenciaCardiaca = reader.IsDBNull(FactorReumatoideoPosicion) ? "" : reader.GetInt32(FactorReumatoideoPosicion).ToString(),
                                        IMC = reader.IsDBNull(IMCPosicion) ? "" : reader.GetFloat(IMCPosicion).ToString(),
                                        Peso = reader.IsDBNull(PesoPosicion) ? "" : reader.GetFloat(PesoPosicion).ToString(),
                                        Talla = reader.IsDBNull(TallaPosicion) ? "" : reader.GetFloat(TallaPosicion).ToString(),
                                        Temperatura = reader.IsDBNull(TemperaturaPosicion) ? "" : reader.GetFloat(TemperaturaPosicion).ToString(),
                                        TensionArterial = reader.IsDBNull(TensionArterialPosicion) ? "" : reader.GetInt32(TensionArterialPosicion).ToString(),
                                    },
                                    Medico = new MedicoAdoDTOResponse()
                                    {
                                        Nombres = reader.IsDBNull(NombresMedicoPosicion) ? "" : reader.GetString(NombresMedicoPosicion),
                                        Apellidos = reader.IsDBNull(ApellidosMedicosPosicion) ? "" : reader.GetString(ApellidosMedicosPosicion),
                                        Correo = reader.IsDBNull(CorreoPosicion) ? "" : reader.GetString(CorreoPosicion),
                                        CedulaProfecional = reader.IsDBNull(CedulaProfecionalPosicion) ? "" : reader.GetInt32(CedulaProfecionalPosicion).ToString(),
                                        Genero = reader.IsDBNull(GeneroPosicion) ? "" : reader.GetString(GeneroPosicion),
                                        Telefono = reader.IsDBNull(TelefonoPosicion) ? "" : reader.GetString(TelefonoPosicion),
                                        RefEspecialidad = new EspecialidadDTOResponse()
                                        {
                                            NombreEspecialidad = reader.IsDBNull(NombreEspecialidadPosicion) ? "" : reader.GetString(NombreEspecialidadPosicion)
                                        }
                                    }
                                };
                                if (recetario.Id == 0)
                                {
                                    throw new InvalidOperationException("No se encontro ningun registro");
                                }
                                else
                                {
                                    response.Data = recetario;
                                    response.Success = true;
                                }
                            }
                        }
                    }
                }
                catch (Exception ex)
                {

                    response.ErrorMessage = "No se encontro ningun registro";
                    _logger.LogCritical(ex, "{ErrorMessage}{Message}", response.ErrorMessage, ex.Message);
                }
            return response;

        }

        public async Task<BaseResponse> DeletRecetaAdoAsync(int id)
        {
            var response = new BaseResponse();

            using (var connectinDB = new SqlConnection(_connection.ClinicaDB))
            {
                try
                {
                    await connectinDB.OpenAsync();

                    var recetaFromDB = 0;

                    SqlCommand cmd = new SqlCommand("uspDeleteReceta", connectinDB);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@Id", id);
                    recetaFromDB = await cmd.ExecuteNonQueryAsync();

                    if (recetaFromDB == 0)
                    {
                        throw new InvalidOperationException("Error al eliminar receta");
                    }
                    else
                    {
                        response.Success = true;
                    }
                }
                catch (Exception ex)
                {

                    response.ErrorMessage = "Error al eliminar receta";
                    _logger.LogCritical(ex,"{ErrorMessage}{Message}",response.ErrorMessage, ex.Message);
                    await connectinDB.CloseAsync();
                }
                return response;
            }

        }

        public async Task<BaseResponse> ReactivarRecetaAdoAsync(int id)
        {
            var response = new BaseResponse();

            using (var connectionDB = new SqlConnection(_connection.ClinicaDB))
            {
                try
                {
                    await connectionDB.OpenAsync();  

                    var RecetaFromDB = 0;

                    SqlCommand cmd = new SqlCommand("uspReactivarReceta", connectionDB);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@Id", id);
                    RecetaFromDB = await cmd.ExecuteNonQueryAsync();

                    if(RecetaFromDB == 0)
                    {
                        throw new InvalidOperationException("Error al Reactivar receta");
                    }
                    else
                    {
                        response.Success = true;
                    }


                }
                catch (Exception ex)
                {

                    response.ErrorMessage = "Errol al reactivar receta";
                    _logger.LogCritical(ex, "{ErrorMessage}{Message}", response.ErrorMessage, ex.Message);
                    await connectionDB.CloseAsync();
                }
                return response;
            }

        }
        public async Task<BaseResponseGeneric<RecetarioAdoDTOResponse>> ListDeleteRecetaAdoAync()
        {
            var response = new BaseResponseGeneric<RecetarioAdoDTOResponse>();


            using (var connectionDB = new SqlConnection(_connection.ClinicaDB)) 
            {
                try
                {
                    await connectionDB.OpenAsync();

                    SqlCommand cmd = new SqlCommand("uspGetDeletRecetasList ", connectionDB);
                    cmd.CommandType = CommandType.StoredProcedure;

                    using (var reader = await cmd.ExecuteReaderAsync(CommandBehavior.SingleResult))
                    {
                    
                        if( reader is not null)
                        {
                            int Idposicion = reader.GetOrdinal("Id");
                            int PrescripcionPosicion = reader.GetOrdinal("Prescripcion");
                            int MedicoIdPosicion = reader.GetOrdinal("MedicoId");
                            int PacienteIdPosicion = reader.GetOrdinal("PacienteId");


                            while(await reader.ReadAsync())
                            {
                                var Receta = new RecetarioAdoDTOResponse()
                                {
                                    Id = reader.IsDBNull(Idposicion) ? 0 : reader.GetInt32(Idposicion),
                                    Prescripcion = reader.IsDBNull(PrescripcionPosicion) ? "" : reader.GetString(PrescripcionPosicion),
                                    Medico = new MedicoAdoDTOResponse()
                                    {
                                        Id = reader.IsDBNull(MedicoIdPosicion) ? 0 : reader.GetInt32(MedicoIdPosicion),
                                    },
                                    Paciente = new PacienteAdoDTOResponse()
                                    {
                                        Id = reader.IsDBNull(PacienteIdPosicion) ? 0 : reader.GetInt32(PacienteIdPosicion),
                                    },

                                };
                                if(Receta.Id == 0)
                                {
                                    throw new InvalidOperationException("Error al listar registros eliminados");
                                }
                                else
                                {
                                    response.Data = Receta;
                                    response.Success = true;
                                    
                                }
                            }
                        }
                    }
                }
                catch (Exception ex)
                {

                    response.ErrorMessage = "Error al liustar registros eliminados";
                    _logger.LogCritical(ex, "{ErrorMessage}{Message}", response.ErrorMessage, ex.Message);
                    await connectionDB.CloseAsync();
                }
                return response;

            }

        }

        public async void ComposeContent(IContainer container, int id)
        {
            throw new NotImplementedException();
        }
    }
}
