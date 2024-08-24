using AutoMapper;
using Clinica.Entities;
using Clinica.Models.Request;
using Clinica.Models.Response;
using Clinica.Repositories.Interfaces;
using Clinica.Services.Implementacion.GenerarPDF;
using Clinica.Services.Interfaces;

using Microsoft.Extensions.Logging;
using QuestPDF.Infrastructure;

using System.Linq.Expressions;





namespace Clinica.Services.Implementacion
{
    public class RecetarioServices : IRecetarioServices,IDocument
    {
        private readonly IRecetarioRepository _repository;
        private readonly ILogger<RecetarioServices> _logger;
        private readonly IMapper _mapper;

        public RecetarioServices(IRecetarioRepository repository,
            ILogger<RecetarioServices> logger,
            IMapper mapper)
        {
            _repository = repository;
            _logger = logger;
            _mapper = mapper;
        }

        public async Task<BaseResponse> AddAsync(RecetarioDTORequest recetarioRequest)
        {
            var response = new BaseResponse();

            try
            {
                await _repository.AddAsync(_mapper.Map<Recetario>(recetarioRequest));
                response.Success = true;
            }
            catch (Exception ex)
            {

                response.ErrorMessage = "Error al Geenerar Receta";
                _logger.LogCritical(ex, "{ErrorMessage} {Message}", response.ErrorMessage, ex.Message);
            }
            return response;

        }


        public async Task<BaseResponseGeneric<RecetarioDTOResponse>> FindByIdAsync(int id)
        {
            var response = new BaseResponseGeneric<RecetarioDTOResponse>();
            try
            {
                Expression<Func<Recetario, bool>> predicate = recetarioDB => recetarioDB.Id.Equals(id);


                var recetario = await _repository.FindByIdAsync(predicate: predicate,
                    selector: x => _mapper.Map<RecetarioDTOResponse>(x),
                    relationships: "Medico,Paciente");

                response.Data = recetario;
                response.Success = true;

                //var document = new RecetaDocument(response.Data);
            }
            catch (Exception ex)
            {

                response.ErrorMessage = "Error al encontrar pacinete";
                _logger.LogCritical(ex, "{ErrorMessage} {Message}", response.ErrorMessage, ex.Message);

            }
            return response;
        }




        public async Task<BaseResponse> UpdateAsync(int id, RecetarioDTORequest recetarioRequest)
        {
            var response = new BaseResponse();

            try
            {
                var registro = await _repository.FindByIdAsync(id);

                if(registro is not null)
                {
                    _mapper.Map(recetarioRequest, registro);

                    await _repository.UpdateAsync();
                }

                response.Success = registro != null;


            }
            catch (Exception ex)
            {

                response.ErrorMessage = "Error al actualizar receta";

                _logger.LogCritical(ex, "{ErrorMessage} {Message}", response.ErrorMessage, ex.Message);

            }

            return response;
        }


        public void Compose(IDocumentContainer container)
        {
            throw new NotImplementedException();
        }


    }
}
