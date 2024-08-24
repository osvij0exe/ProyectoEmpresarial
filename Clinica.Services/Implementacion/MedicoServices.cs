using AutoMapper;
using Clinica.Entities;
using Clinica.Models.Request;
using Clinica.Models.Response;
using Clinica.Repositories.Interfaces;
using Clinica.Services.Helpers;
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
    public class MedicoServices : IMedicoServices
    {
        private readonly IMedicoRepository _repository;
        private readonly ILogger<MedicoServices> _logger;
        private readonly IMapper _mapper;

        public MedicoServices(IMedicoRepository repository,
            ILogger<MedicoServices> logger,
            IMapper mapper)
        {
            _repository = repository;
            _logger = logger;
            _mapper = mapper;
        }

        public async Task<PaginationResponse<MedicoDTOResponse>> ListAsync(string? filtroNombres, string? filtroApellidos, int page, int rows)
        {
            var response = new PaginationResponse<MedicoDTOResponse>();

            try
            {
                Expression<Func<Medico, bool>> predicate = medicoDB => medicoDB.Nombres.Contains(filtroNombres ?? string.Empty)
                                            && medicoDB.Apellidos.Contains(filtroApellidos ?? string.Empty);

                var tupla = await _repository.ListAsync(
                    predicate: predicate,
                    selector: x => _mapper.Map<MedicoDTOResponse>(x),
                    orderBy: medicoDB => medicoDB.Apellidos,
                    relationships: "Especialidad",
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

                response.ErrorMessage = "Error al listar a los Medicos";
                _logger.LogCritical(ex, "{ErrorMessage} {Message}", response.ErrorMessage, ex.Message);
            
            }
            return response;
        }

        public async Task<BaseResponseGeneric<MedicoDTOResponse>> FindByIdAsync(int id)
        {
            var response = new BaseResponseGeneric<MedicoDTOResponse>();

            try
            {
                Expression<Func<Medico, bool>> predicate = medicoDB => medicoDB.Id.Equals(id);


                var medico = await _repository.FindByIdAsync(predicate: predicate,
                    selector: x => _mapper.Map<MedicoDTOResponse>(x),
                    relationships:"Especialidad");

                response.Data = medico;
                response.Success= true;

            }
            catch (Exception ex)
            {

                response.ErrorMessage = "Error al listar a los Medicos";
                _logger.LogCritical(ex, "{ErrorMessage} {Message}", response.ErrorMessage, ex.Message);

            }
            return response;

        }

        public async Task<BaseResponse> AddAsync(MedicoDTORequest medicoRequest)
        {
            var response = new BaseResponse();

            try
            {

                //var newMedico = _mapper.Map<Medico>(medicoRequest);

                //if(newMedico.Genero == newMedico.Genero.ToLower())
                //{
                //    newMedico.Genero =  newMedico.Genero.ToUpper();
                //}

                


                medicoRequest.Genero = ConvertCharacters.verifyUpperCharacter(medicoRequest.Genero);

                

                await _repository.AddAsync(_mapper.Map<Medico>(medicoRequest));
                response.Success = true;
            }
            catch (Exception ex)
            {

                response.ErrorMessage = "Error al listar a los Medicos";
                _logger.LogCritical(ex, "{ErrorMessage} {Message}", response.ErrorMessage, ex.Message);

            }

            return response;
        }

        public async Task<BaseResponse> UpdateAsync(int id, MedicoDTORequest medicorequest)
        {
            var response = new BaseResponse();

            try
            {
                var registro = await _repository.FindByIdAsync(id);

                if(registro is not null)
                {
                    _mapper.Map(medicorequest , registro);

                    await _repository.UpdateAsync();
                }
                response.Success = registro != null;
            }
            catch (Exception ex)
            {

                response.ErrorMessage = "Error al listar a los Medicos";
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

                response.ErrorMessage = "Error al listar a los Medicos";
                _logger.LogCritical(ex, "{ErrorMessage} {Message}", response.ErrorMessage, ex.Message);

            }
            return response;
        }

        public async Task<BaseResponseGeneric<ICollection<MedicoDTOResponse>>> ListarEliminadosAsync()
        {
            var response = new BaseResponseGeneric<ICollection<MedicoDTOResponse>>();

            try
            {

                var medicosEliminados = await _repository.ListarEliminados();

                response.Data = _mapper.Map<ICollection<MedicoDTOResponse>>(medicosEliminados);
                response.Success = true;
            }
            catch (Exception ex)
            {

                response.ErrorMessage = "Error al listar a los Medicos";
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

                response.ErrorMessage = "Error al listar a los Medicos";
                _logger.LogCritical(ex, "{ErrorMessage} {Message}", response.ErrorMessage, ex.Message);

            }
            return response;
        }


    }
}
