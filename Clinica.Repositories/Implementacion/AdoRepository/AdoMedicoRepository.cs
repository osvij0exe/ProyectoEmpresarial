using Clinica.AccesoADatos.JsonSettings;
using Clinica.Entities;
using Clinica.Models.Request;
using Clinica.Models.Response;
using Clinica.Models.Response.MedicoResponse;
using Clinica.Repositories.Interfaces.IAdoRepository;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System.Data;

namespace Clinica.Repositories.Implementacion.AdoRepository
{
    public class AdoMedicoRepository : IAdoMedicoRepository
    {
        private readonly ConnectionSetting _connection;
        private readonly ILogger<AdoMedicoRepository> _logger;

        public AdoMedicoRepository(IOptions<ConnectionSetting> connection,
            ILogger<AdoMedicoRepository> logger)
        {
            _connection = connection.Value;
            _logger = logger;
        }



        public async Task<BaseResponse> AddMedicoAsync(MedicoDTORequest request)
        {
            var response = new BaseResponse();

            using (var connectionDB = new SqlConnection(_connection.ClinicaDB))
            {
                try
                {
                    int sqlResponse = 0;

                    await connectionDB.OpenAsync();
                    SqlCommand cmd = new SqlCommand("uspInserMedico", connectionDB);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@Nombres", request.Nombres);
                    cmd.Parameters.AddWithValue("@Apellidos", request.Apellidos);
                    cmd.Parameters.AddWithValue("@EspecialidadId", request.EspecialidadId);
                    cmd.Parameters.AddWithValue("@CedulaProfecional", request.CedulaProfecional);
                    cmd.Parameters.AddWithValue("@Correo", request.Correo);
                    cmd.Parameters.AddWithValue("@Genero", request.Genero);
                    cmd.Parameters.AddWithValue("@Telefono", request.Telefono);
                    //para implementar el insert, update y delete (retorna el numero de filas)
                    sqlResponse= await cmd.ExecuteNonQueryAsync();
                    
                    if( sqlResponse != 0)
                    {
                        response.Success = true;

                    }
                    else
                    {
                        throw new InvalidOperationException("No se pudo agregar correctamente el medico");
                    }

                }
                catch (Exception ex)
                {

                    response.ErrorMessage = "Error al insertar Medico";
                    _logger.LogCritical(ex, "{ErrorMessage}{Message}", response.ErrorMessage, ex.Message);
                    await connectionDB.CloseAsync();
                }


                return response; 
            }

        }

        public async Task<BaseResponse> UpdateMedicoAsync(int id, MedicoDTORequest request)
        {
            var response = new BaseResponse();

                using (var connection = new SqlConnection(_connection.ClinicaDB))
            {
                try
                {
                    int sqlResponse = 0;
                    await connection.OpenAsync();

                    SqlCommand cmd = new SqlCommand("uspUpdateMedico", connection);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@Id", id.Equals(0) ? "No se encontro ningun registro" : id);
                    cmd.Parameters.AddWithValue("@Nombres", request.Nombres);
                    cmd.Parameters.AddWithValue("@Apellidos", request.Apellidos);
                    cmd.Parameters.AddWithValue("@EspecialidadId", request.EspecialidadId);
                    cmd.Parameters.AddWithValue("@CedulaProfecional", request.CedulaProfecional);
                    cmd.Parameters.AddWithValue("@Correo", request.Correo);
                    cmd.Parameters.AddWithValue("@Genero", request.Genero);
                    cmd.Parameters.AddWithValue("@Telefono", request.Telefono);
                    sqlResponse = await cmd.ExecuteNonQueryAsync();

                    var ExisteRegistro = await FindByIdAsync(id);
                    
                    if(sqlResponse != 0 && ExisteRegistro is not null)
                    {
                        response.Success = ExisteRegistro != null;
                    }
                    else
                    {
                        throw new Exception($"No se encontro ningun registro con el id = {id}");
                    }


                }
                catch (Exception ex)
                {

                    response.ErrorMessage = "Error al Actualizar el registro";
                    _logger.LogCritical(ex, "{ErrorMessage}{Message}", response.ErrorMessage, ex.Message);
                    await connection.CloseAsync();
                }
                return response;
            }
        }

        public async Task<BaseResponseGeneric<MedicoAdoDTOResponse>> FindByIdAsync(int id)
        {
                var response = new BaseResponseGeneric<MedicoAdoDTOResponse>();
            
                using ( var connectionDB = new SqlConnection(_connection.ClinicaDB))
                try
                {
                    var medico = new MedicoAdoDTOResponse();

                    await connectionDB.OpenAsync();
                    SqlCommand cmd = new SqlCommand("uspFindByIDMedico", connectionDB);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@Id", id.Equals(0) ? "No se encontro ningun registro" : id);
                 
                    using (var reader = await cmd.ExecuteReaderAsync(CommandBehavior.SingleResult))
                    {
                        if(reader is not null)
                        {
                            medico = new MedicoAdoDTOResponse();
                            int IdPosicion = reader.GetOrdinal("Id");
                            int NombresPosicion   = reader.GetOrdinal("Nombres");
                            int ApellidosPosicion = reader.GetOrdinal("Apellidos");
                            int GeneroPosicion   = reader.GetOrdinal("Genero");
                            int CorreoPosicion   = reader.GetOrdinal("Correo");
                            int EstadoPosicion   = reader.GetOrdinal("Estado");
                            int TelefonoPosicion = reader.GetOrdinal("Telefono");
                            int FechaCreacionPosicion = reader.GetOrdinal("FechaCreacion");
                            int CedulaProfecionalPosicion = reader.GetOrdinal("CedulaProfecional");
                            int NombreEspecialidadPosicion = reader.GetOrdinal("NombreEspecialidad");

                                while (await reader.ReadAsync())
                            
                                medico = new MedicoAdoDTOResponse()
                                {
                                    Id =  reader.IsDBNull(IdPosicion) ? 0 : reader.GetInt32(IdPosicion),
                                    Nombres = reader.IsDBNull(NombresPosicion) ? "" : reader.GetString(NombresPosicion),
                                    Apellidos = reader.IsDBNull(ApellidosPosicion) ? "" : reader.GetString(ApellidosPosicion),
                                    Genero = reader.IsDBNull(GeneroPosicion) ? "" : reader.GetString(GeneroPosicion),
                                    Correo = reader.IsDBNull(CorreoPosicion) ? "" : reader.GetString(CorreoPosicion),
                                    Estado = Convert.ToBoolean(reader.IsDBNull(EstadoPosicion) ? false : reader.GetBoolean(EstadoPosicion)),
                                    Telefono = reader.IsDBNull(TelefonoPosicion) ? "" : reader.GetString(TelefonoPosicion),
                                    FechaCreacion = Convert.ToDateTime(reader.IsDBNull(FechaCreacionPosicion) ? (DateTime?)null : reader.GetDateTime(FechaCreacionPosicion)),
                                    CedulaProfecional = reader.IsDBNull(CedulaProfecionalPosicion) ? "" : reader.GetInt32(CedulaProfecionalPosicion).ToString(),
                                    RefEspecialidad = new EspecialidadDTOResponse()
                                    {
                                        NombreEspecialidad = reader.IsDBNull(NombreEspecialidadPosicion) ? "" : reader.GetString(NombreEspecialidadPosicion),
                                    }

                                };
                            if( medico.Id == 0)
                            {

                                throw new InvalidOperationException("No se encontro ningun registro");
                            
                            }
                            else
                            {
                                response.Data = medico;
                                response.Success = true;

                            }
                            await reader.CloseAsync();
                        }
                    }
                }
                catch (Exception ex)
                {
                    response.ErrorMessage = "No se encontro ningun registro";
                    _logger.LogCritical(ex, "{ErrorMessage}{Message}", response.ErrorMessage, ex.Message);
                    await connectionDB.CloseAsync();
                }
            return response; 
        }

        public async Task<BaseResponseGeneric<List<MedicoAdoDTOResponse>>> GetAdoMedicosAsync(string? nombres, string? apellidos, string? cedulaProfecional, string? Especialidad)
        {
            var response = new BaseResponseGeneric<List<MedicoAdoDTOResponse>>();
            
            using (var connectionDB = new SqlConnection(_connection.ClinicaDB))
                try
                {
                    var ListarMedicos = new List<MedicoAdoDTOResponse>();
                    
                    await connectionDB.OpenAsync();


                    SqlCommand cmd = new SqlCommand("uspObtenerMedicos", connectionDB);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@Nombres", nombres is null ? "" : nombres);
                    cmd.Parameters.AddWithValue("@Apellidos", apellidos is null ? "" : apellidos);
                    cmd.Parameters.AddWithValue("@CedulaProfecional", cedulaProfecional is null ? "" : cedulaProfecional);
                    cmd.Parameters.AddWithValue("@NombreEspecialidad", Especialidad is null ? "" : Especialidad);

                    using (var reader = await cmd.ExecuteReaderAsync(CommandBehavior.SingleResult))
                    {
                        if (reader is not null)
                        {
                            ListarMedicos = new List<MedicoAdoDTOResponse>();
                            //Ejemplo indicando poscionen en la lista
                            int IdPosicion = reader.GetOrdinal("Id");
                            int NombresPosicion = reader.GetOrdinal("Nombres");
                            int ApellidosPosicion = reader.GetOrdinal("Apellidos");
                            int GeneroPosicion = reader.GetOrdinal("Genero");
                            int CorreoPosicion = reader.GetOrdinal("Correo");
                            int TelefonoPosicion = reader.GetOrdinal("Telefono");
                            int FechaCreacionPosicion = reader.GetOrdinal("FechaCreacion");
                            int CedulaProfesionalPosicion = reader.GetOrdinal("CedulaProfecional");
                            int EstadoPosicion = reader.GetOrdinal("Estado");
                            int NombreEspecialidadPosicion = reader.GetOrdinal("NombreEspecialidad");

                            while (await reader.ReadAsync())
                            {
                                ListarMedicos.Add(new MedicoAdoDTOResponse()
                                {
                                    //Ejemplo como implementar la posicion
                                    Id = reader.IsDBNull(IdPosicion) ? 0 : reader.GetInt32(IdPosicion),
                                    Nombres = reader.IsDBNull(NombresPosicion) ? "" : reader.GetString(NombresPosicion),
                                    Apellidos = reader.IsDBNull(ApellidosPosicion) ? "" : reader.GetString(ApellidosPosicion),
                                    Genero = reader.IsDBNull(GeneroPosicion) ? "" : reader.GetString(GeneroPosicion),
                                    Correo = reader.IsDBNull(CorreoPosicion) ? "" : reader.GetString(CorreoPosicion),
                                    Telefono = reader.IsDBNull(TelefonoPosicion) ? "" : reader.GetString(TelefonoPosicion),
                                    FechaCreacion = Convert.ToDateTime(reader.IsDBNull(FechaCreacionPosicion)
                                                    ? (DateTime?)null : reader.GetDateTime(FechaCreacionPosicion)),
                                    CedulaProfecional = reader.IsDBNull(CedulaProfesionalPosicion) ? "" : reader.GetInt32(CedulaProfesionalPosicion).ToString(),
                                    Estado = Convert.ToBoolean(reader.IsDBNull(EstadoPosicion)) ? false : reader.GetBoolean(EstadoPosicion),
                                    RefEspecialidad = new EspecialidadDTOResponse()
                                    {
                                        NombreEspecialidad = reader.IsDBNull(NombreEspecialidadPosicion) ? "" : reader.GetString(NombreEspecialidadPosicion),
                                    }
                                });
                            }
                         await reader.CloseAsync();
                        }

                    }
                    response.Data = ListarMedicos;
                    response.Success = true;
                }
                catch (Exception ex)
                {
               
                    response.ErrorMessage = "Errol al listar Medicos";
                    _logger.LogCritical(ex,"{ErrorMessage} {Message}",response.ErrorMessage,ex.Message);
                    await connectionDB.CloseAsync();
                }
            return response;
        }

        public async Task<BaseResponse> DeleteMedicoAsync(int id)
        {
            var response = new BaseResponse();

            using (var connection = new SqlConnection(_connection.ClinicaDB))
            {
                try
                {
                    int sqlResponse = 0;
                    
                    await connection.OpenAsync();

                    SqlCommand cmd = new SqlCommand("uspLogicDeleteMedico", connection);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@Id", id == 0 ? "" : id);
                    sqlResponse = await cmd.ExecuteNonQueryAsync();

                    var ExisteRegistro = await FindByIdAsync(id);

                    if(sqlResponse != 0 && ExisteRegistro is not null)
                    {
                        response.Success = true;
                    }
                    else
                    {
                        throw new InvalidOperationException($"No se encontro ningun registro con id = {id}");
                    }



                }
                catch (Exception ex)
                {

                    response.ErrorMessage = "No se encontro ningun registro";
                    _logger.LogCritical(ex, "{ErrorMessage}{Message}", response.ErrorMessage, ex.Message);
                    await connection.CloseAsync();
                }
                return response;

            }

        }

        public async Task<BaseResponse> ReactivarMedicoAsync(int id)
        {
            var response = new BaseResponse();

            using (var connection = new SqlConnection(_connection.ClinicaDB))
            {
                try
                {
                    var sqlResponse = 0;

                    await connection.OpenAsync();

                    SqlCommand cmd = new SqlCommand("uspReactivarMedico", connection);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@Id", id == 0 ? "" : id);
                    sqlResponse = await cmd.ExecuteNonQueryAsync();

                    var ExisteRegistro = await FindByIdAsync(id);
                    
                    if(sqlResponse != 0 && ExisteRegistro is not null)
                    {
                        response.Success = true;
                    }
                    else
                    {
                         throw new InvalidOperationException(" No se encontro ningun registro");
                    }


                }
                catch (Exception ex)
                {

                    response.ErrorMessage = "Error al reactivar Medico";
                    _logger.LogCritical(ex, "{ErrorMessage}{Message}", response.ErrorMessage, ex.Message);
                    await connection.CloseAsync();
                }

                return response;
            }


        }

        public async Task<BaseResponseGeneric<List<MedicoAdoDTOResponse>>> GetDeleteMedicoListAsync()
        {
            var response = new BaseResponseGeneric<List<MedicoAdoDTOResponse>>();

            using ( var connection = new SqlConnection(_connection.ClinicaDB))
            {
                try
                {
                    await connection.OpenAsync();

                    var ListarMedicos =  new List<MedicoAdoDTOResponse>();

                    SqlCommand cmd = new SqlCommand("uspListDeleteMedicos", connection);
                    cmd.CommandType = CommandType.StoredProcedure;

                    using (var reader = await cmd.ExecuteReaderAsync(CommandBehavior.SingleResult))
                    {
                        if (reader is not null)
                        {
                            ListarMedicos = new List<MedicoAdoDTOResponse>();

                            //int IdPosicion      = reader.GetOrdinal("Id");
                            int NombresPosicion = reader.GetOrdinal("Nombres");
                            int ApellidosPosicion = reader.GetOrdinal("Apellidos");
                            //int GeneroPosicion = reader.GetOrdinal("Genero");
                            //int CorreoPosicion = reader.GetOrdinal("Correo");
                            //int TelefonoPosicion = reader.GetOrdinal("Telefono");
                            //int FechaCreacionPosicion = reader.GetOrdinal("FechaCreacion");
                            //int CedulaProfesionalPosicion = reader.GetOrdinal("CedulaProfecional");
                            //int EstadoPosicion = reader.GetOrdinal("Estado");
                            int NombreEspecialidadPosicion = reader.GetOrdinal("NombreEspecialidad");

                            while(await reader.ReadAsync())
                            {
                                ListarMedicos.Add(new MedicoAdoDTOResponse()
                                {
                                    //Id      = await reader.IsDBNullAsync(IdPosicion) ? 0 : reader.GetInt32(IdPosicion),
                                    Nombres = await reader.IsDBNullAsync(NombresPosicion) ? "" : reader.GetString(NombresPosicion),
                                    Apellidos = await reader.IsDBNullAsync(ApellidosPosicion) ? "" : reader.GetString(ApellidosPosicion),
                                    //Genero    = await reader.IsDBNullAsync(GeneroPosicion) ? "" : reader.GetString(GeneroPosicion),
                                    //Correo    = await reader.IsDBNullAsync(CorreoPosicion) ? "" : reader.GetString(CorreoPosicion),
                                    //Telefono  = await reader.IsDBNullAsync(TelefonoPosicion) ? "" : reader.GetString(TelefonoPosicion),
                                    //FechaCreacion = Convert.ToDateTime( await reader.IsDBNullAsync(FechaCreacionPosicion)
                                    //                ? (DateTime?) null : reader.GetDateTime(FechaCreacionPosicion).ToString()),
                                    //CedulaProfecional = await reader.IsDBNullAsync(CedulaProfesionalPosicion) ? "" : reader.GetString(CedulaProfesionalPosicion),
                                    //Estado    =  Convert.ToBoolean(await reader.IsDBNullAsync(EstadoPosicion)) ? false : reader.GetBoolean(EstadoPosicion),
                                    RefEspecialidad = new EspecialidadDTOResponse()
                                    {
                                        NombreEspecialidad = await reader.IsDBNullAsync(NombreEspecialidadPosicion) ? "" : reader.GetString(NombreEspecialidadPosicion),
                                    }
                                });

                                response.Data = ListarMedicos;
                                response.Success = true;
                            }
                            await reader.CloseAsync();
                        }
                    }

                }
                catch (Exception ex)
                {

                    response.ErrorMessage = "No se encontro ningun registro";
                    _logger.LogCritical(ex, "{ErrorMessage}{Message}", response.ErrorMessage, ex.Message);
                    await connection.CloseAsync();
                }
                return response;
                
            }

        }
    }
}
