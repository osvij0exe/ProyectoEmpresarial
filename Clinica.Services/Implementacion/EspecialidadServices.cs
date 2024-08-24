using AutoMapper;
using Clinica.Entities;
using Clinica.Models.Response;
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
    public class EspecialidadServices : IEspecialidadServices
    {
        private readonly IEspecialidadRepository _repository;
        private readonly ILogger<EspecialidadServices> _logger;
        private readonly IMapper _mapper;

        public EspecialidadServices(IEspecialidadRepository repository,
            ILogger<EspecialidadServices> logger,
            IMapper mapper)
        {
            _repository = repository;
            _logger = logger;
            _mapper = mapper;
        }

        public async Task<PaginationResponse<EspecialidadDTOResponse>> ListAsync(string? filtroNombresEspecialidad, int page, int rows)
        {
            var response = new PaginationResponse<EspecialidadDTOResponse>(); ;

            try
            {
                Expression<Func<Especialidad, bool>> predicate = especialidadDB => especialidadDB.NombreEspecialidad.Contains(filtroNombresEspecialidad ?? string.Empty);

                var tupla = await _repository.ListAsync(predicate: predicate,
                    selector: x => _mapper.Map<EspecialidadDTOResponse>(x),
                    orderBy: especialidadDB => especialidadDB.NombreEspecialidad,
                    relationships: "",
                    page,
                    rows);


                response.Data = tupla.Collection;
                response.TotalPaginas = tupla.Total / rows;

                if (tupla.Total % rows > 0)
                {
                    response.TotalPaginas++;
                }
                response.Success = true;


            }
            catch (Exception ex)
            {

                response.ErrorMessage = "Error al listar especialidades";
                _logger.LogCritical(ex, "{ErrorMessage} {Message}", response.ErrorMessage, ex.Message);
            }
            return response;

        }

        public async Task<BaseResponseGeneric<EspecialidadDTOResponse>> FindByIdAsync(int id)
        {
            var response = new BaseResponseGeneric<EspecialidadDTOResponse>();
            try
            {
                Expression<Func<Especialidad, bool>> predicate = EspecialidadDB => EspecialidadDB.Id.Equals(id);

                var especialidad = await _repository.FindByIdAsync(predicate: predicate,
                    selector: x => _mapper.Map<EspecialidadDTOResponse>(x));

                response.Data = especialidad;
                response.Success = true;


            }
            catch (Exception ex)
            {

                response.ErrorMessage = "Error al encontra especialidad";
                _logger.LogCritical(ex, "{ErrorMessage} {Message}", response.ErrorMessage, ex.Message);

            }
            return response;



        }
        //listar sin filtros
        public async Task<BaseResponseGeneric<ICollection<EspecialidadDTOResponse>>> ListAsync()
        {
            var response = new BaseResponseGeneric<ICollection<EspecialidadDTOResponse>>();

            try
            {
                var data = await _repository.ListAsync(null);

                response.Data = data
                    .Select( _mapper.Map<EspecialidadDTOResponse>)
                    .ToList();
            }
            catch (Exception ex)
            {

                response.ErrorMessage = "Error al encontra especialidad";
                _logger.LogCritical(ex, "{ErrorMessage} {Message}", response.ErrorMessage, ex.Message);
            }
            return response;
        }
    }
}
