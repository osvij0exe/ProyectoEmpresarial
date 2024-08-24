using AutoMapper;
using Azure;
using Clinica.Entities;
using Clinica.Models.Request;
using Clinica.Models.Response;
using Clinica.Repositories.Implementacion;
using Clinica.Repositories.Interfaces;
using Clinica.Services.Interfaces;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Clinica.Services.Implementacion
{
    public class ConsultaServices : IConsultaServicescs
    {
        private readonly IConsultaRepository _repository;
        private readonly ILogger<ConsultaServices> _logger;
        private readonly IMapper _mapper;

        public ConsultaServices(IConsultaRepository repository,
            ILogger<ConsultaServices> logger,
            IMapper mapper)
        {
            _repository = repository;
            _logger = logger;
            _mapper = mapper;
        }


        public async Task<PaginationResponse<ConsultaDTOResponse>> ListAsync(string? filtroFechaCita, 
            string? NombreMedico, string? ApellidoMedico,
            string? NombrePaciente, string? ApellidoPaciente,
            int? especialidadId,int? situacionConsulta,
            int page, int rows)
        {
            var response = new PaginationResponse<ConsultaDTOResponse>();

            try
            {
                Expression<Func<Consulta, bool>> predicate = consultaDB => consultaDB.FechaCita.ToString().Contains(filtroFechaCita ?? string.Empty)
                                                && consultaDB.Medico.Nombres.Contains(NombreMedico ?? string.Empty)
                                                && consultaDB.Medico.Apellidos.Contains(ApellidoMedico ?? string.Empty)
                                                && consultaDB.Paciente.Nombres.Contains(NombrePaciente ?? string.Empty)
                                                && consultaDB.Paciente.Apellidos.Contains(ApellidoPaciente ?? string.Empty)
                                                && consultaDB.EspecialidadId.ToString().Contains(especialidadId.ToString() ?? string.Empty);
                                                //&& (situacionConsulta == null || consultaDB.Situacion == (SituacionConsulta)situacionConsulta); TODO: factorizar
                 //refactorizar
                if (situacionConsulta != null)
                {
                    predicate = consultaDB => consultaDB.FechaCita.ToString().Contains(filtroFechaCita ?? string.Empty)
                                                && consultaDB.Medico.Nombres.Contains(NombreMedico ?? string.Empty)
                                                && consultaDB.Medico.Apellidos.Contains(ApellidoMedico ?? string.Empty)
                                                && consultaDB.Paciente.Nombres.Contains(NombrePaciente ?? string.Empty)
                                                && consultaDB.Paciente.Apellidos.Contains(ApellidoPaciente ?? string.Empty)
                                                && consultaDB.EspecialidadId.ToString().Contains(especialidadId.ToString() ?? string.Empty)

                                                && consultaDB.Situacion == (SituacionConsulta)situacionConsulta.Value;
                }

                var tupla = await _repository.ListAsync<ConsultaDTOResponse, string>(
                        predicate: predicate,
                        selector: x => _mapper.Map<ConsultaDTOResponse>(x),
                        orderBy: consultaDB => consultaDB.FechaCita.ToString(),
                        relationships: "Medico,Paciente,Especialidad",
                        page,
                        rows
                        );

                    response.Data = tupla.Collection;
                    response.TotalPaginas = tupla.Total / rows;

                    if(tupla.Total % rows > 0)
                    {
                        response.TotalPaginas++;
                    }
                    response.Success = true;

            }
            catch (Exception ex)
            {

                response.ErrorMessage = "Error al listar las consultas";
                _logger.LogCritical(ex, "{ErrorMessage} {Message}", response.ErrorMessage, ex.Message);
                
            }
            return response;

        }

        public async Task<BaseResponseGeneric<ConsultaDTOResponse>> FindByIdAsync(int id)
        {
            var response = new BaseResponseGeneric<ConsultaDTOResponse>();

            try
            {
                await _repository.FindByIdAsync(id);
                response.Success = true;
            }
            catch (Exception ex)
            {

                response.ErrorMessage = "Error al buscar consulta";
                _logger.LogCritical(ex, "{ErrorMessage} {Message}",response.ErrorMessage,ex.Message);
         
            }
            return response;

        }
        public async Task<BaseResponse> UpdateAsync(int id, ConsultaDTORequest consultaRequest)
        {
            var response = new BaseResponse();

            try
            {
                var registro = await _repository.FindByIdAsync(id);
                if(registro is not null)
                {
                    _mapper.Map(consultaRequest, registro);

                    await _repository.UpdateAsync();
                }
                response.Success = registro != null;
            }
            catch (Exception ex)
            {
                
                response.ErrorMessage = "Error al actualizar consulta";
                _logger.LogCritical(ex, "{ErrorMessage} {Message}", response.ErrorMessage, ex.Message);

            }
            return response;
        }

        public async Task<BaseResponse> AddAsync(ConsultaDTORequest consultaRequest)
        {
            var response = new BaseResponse();

            try
            {
                await _repository.AddAsync(_mapper.Map<Consulta>(consultaRequest));
                response.Success = true;
            }
            catch (Exception ex)
            {

                response.ErrorMessage = "Error al ingresar consulta";
                _logger.LogCritical(ex, "{ErrorMessage} {Message}", response.ErrorMessage, ex.Message);
            }
            return response;
        }

        public async Task<BaseResponse> DeleteAsync(int id)
        {
            var response = new BaseResponse();
            try
            {
                await _repository.DeleteAsync(id);
                response.Success = true;
            }
            catch (Exception ex)
            {

                response.ErrorMessage = "Error al ingresar consulta";
                _logger.LogCritical(ex, "{ErrorMessage} {Message}", response.ErrorMessage, ex.Message);
            }
            return response;

        }

        public async Task<BaseResponseGeneric<ICollection<ConsultaDTOResponse>>> ListarEliminadosAsync()
        {
            var response = new BaseResponseGeneric<ICollection<ConsultaDTOResponse>>();

            try
            {
                var consultasEliminados = await _repository.ListarEliminados();

                response.Data = _mapper.Map<ICollection<ConsultaDTOResponse>>(consultasEliminados);
                response.Success = true;
            }
            catch (Exception ex)
            {

                response.ErrorMessage = "Error al listar consultas consulta";
                _logger.LogCritical(ex, "{ErrorMessage} {Message}", response.ErrorMessage, ex.Message);
            }
            return response;
        }


        public async Task<BaseResponse> ReactivarAsync(int id)
        {
            var response = new BaseResponse();
            try
            {
                await _repository.Reactivar(id);
                response.Success= true;
            }
            catch (Exception ex)
            {

                response.ErrorMessage = "Error al ingresar consulta";
                _logger.LogCritical(ex, "{ErrorMessage} {Message}", response.ErrorMessage, ex.Message);
            }
            return response;
        }

    }
}
