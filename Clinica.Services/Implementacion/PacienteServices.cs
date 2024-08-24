using AutoMapper;
using Clinica.Entities;
using Clinica.Models.Request;
using Clinica.Models.Response;
using Clinica.Models.Response.PacienteResponse;
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
    public class PacienteServices : IPacienteServices
    {
        private readonly IPacienteRepository _repository;
        private readonly ILogger<PacienteServices> _logger;
        private readonly IMapper _mapper;

        public PacienteServices(IPacienteRepository repository,
            ILogger<PacienteServices> logger,
            IMapper mapper)
        {
            _repository = repository;
            _logger = logger;
            _mapper = mapper;
        }

        public async Task<PaginationResponse<PacienteDTOResponse>> ListAsync(string? filtroNombres, string? filtroApellidos, int page, int rows)
        {
            var response = new PaginationResponse<PacienteDTOResponse>();

            try
            {

                Expression<Func<Paciente, bool>> predicate = pacienteDB => pacienteDB.Nombres.Contains(filtroNombres ?? string.Empty)
                                                && pacienteDB.Apellidos.Contains(filtroApellidos ?? string.Empty);


                var tupla = await _repository.ListAsync(
                    predicate: predicate,
                    selector: x => _mapper.Map<PacienteDTOResponse>(x),
                    orderBy: pacienteDB => pacienteDB.Apellidos,
                    relationships: "",
                    page,
                    rows);

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
                response.ErrorMessage = "Error al listar a los pacientes";
                _logger.LogCritical(ex, "{ErrorMessage} {Message}", response.ErrorMessage, ex.Message);
                
            }
            return response;
        }


        public async Task<BaseResponseGeneric<PacienteDTOResponse>> FindByIdAsync(int id)
        {
            var response = new BaseResponseGeneric<PacienteDTOResponse>();
            try
            {
                Expression<Func<Paciente, bool>> predicate = pacienteDB => pacienteDB.Id.Equals(id);


                var paciente = await _repository.FindByIdAsync(predicate: predicate,
                    selector: x => _mapper.Map<PacienteDTOResponse>(x));

                response.Data = paciente;
                response.Success = true;

            }
            catch (Exception ex)
            {

                response.ErrorMessage = "Error al encontrar pacinete";
                _logger.LogCritical(ex, "{ErrorMessage} {Message}", response.ErrorMessage, ex.Message);
                
            }
            return response;
        }
        public async Task<BaseResponse> AddAsync(PacienteDTORequest pacienteRequest)
        {
            var response = new BaseResponse();

            try
            {
                await _repository.AddAsync(_mapper.Map<Paciente>(pacienteRequest));
                response.Success = true;
            }
            catch (Exception ex)
            {

                response.ErrorMessage = "Error al ingresar pacinete";
                _logger.LogCritical(ex, "{ErrorMessage} {Message}", response.ErrorMessage, ex.Message);

            }
            return response;


        }
        public async Task<BaseResponse> UpdateAsync(int id, PacienteDTORequest pacienteRequest)
        {
            var response = new BaseResponse();

            try
            {
                var registro = await _repository.FindByIdAsync(id);
                if(registro is not null)
                {
                    _mapper.Map(pacienteRequest,registro);

                    await _repository.UpdateAsync();
                }
                response.Success = registro != null;
                

            }
            catch (Exception ex)
            {

                response.ErrorMessage = "Error al Actualizar a los Paciente";
                _logger.LogCritical(ex, "{ErrorMessage} {Message}", response.ErrorMessage, ex.Message);

            }
            return response;
        }
        public async Task<BaseResponse> PatchAsync(int id, PacienteDTOPatch pacienteRequest)
        {
            var response = new BaseResponse();
            try
            {
                var registro = await _repository.FindByIdAsync(id);
                if (registro is not null)
                {
                    _mapper.Map(pacienteRequest, registro);

                    await _repository.UpdateAsync();
                }
                response.Success = registro != null;

            }
            catch (Exception ex)
            {

                response.ErrorMessage = "Error al reactivar Paciente";
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

                response.ErrorMessage = "Error al eliminar Paciente";
                _logger.LogCritical(ex, "{ErrorMessage} {Message}", response.ErrorMessage, ex.Message);

            }
            return response;
        }



        public async Task<BaseResponseGeneric<ICollection<PacienteDTOResponse>>> ListarEliminadosAsync()
        {
            var response = new BaseResponseGeneric<ICollection<PacienteDTOResponse>>();
            
            try
            {

                var pacienteEliminados = await _repository.ListarEliminados();

                response.Data = _mapper.Map<ICollection<PacienteDTOResponse>>(pacienteEliminados);
                response.Success = true;

            }
            catch (Exception ex)
            {

                response.ErrorMessage = "Error al listar a los Pacientre Eliminados";
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
                response.Success = true;


            }
            catch (Exception ex)
            {

                response.ErrorMessage = "Error al reactivar Paciente";
                _logger.LogCritical(ex, "{ErrorMessage} {Message}", response.ErrorMessage, ex.Message);

            }
            return response;
        }


    }
}
