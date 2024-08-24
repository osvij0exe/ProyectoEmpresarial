using Clinica.Entities;
using Clinica.Models;
using Clinica.Models.Request;
using Clinica.ViewModels;
using Clinica.WebMVC.Services.Implementaciones;
using Clinica.WebMVC.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Clinica.WebMVC.Controllers
{
    public class ConsultaController : Controller
    {
        private readonly IConsultaProxy _proxys;
        private readonly IEspecialidadProxy _especialidadProxy;

        public ConsultaController(IConsultaProxy proxys, IEspecialidadProxy especialidadProxy)
        {
            _proxys = proxys;
            _especialidadProxy = especialidadProxy;
        }

        //Get
        public async Task<IActionResult> Index(ConsultaViewModel model) {

            PaginationData pager = ViewBag.Pager != null
                ? ViewBag.Pager
                : new PaginationData();
            if (pager.CurrentPage == 0)
            {
                pager.CurrentPage = model.Page <= 0 ? 1 : model.Page;
            }

            pager.RowsPerPage = model.Rows <= 0 ? 5 : model.Rows;


            model.Especialidades = await _especialidadProxy.ListAsync();

            var response = await _proxys.ListAsync(new BusquedaConsultaRequest
            {
                NombreMedico = model.NombreMedico,
                ApellidoMedico = model.ApellidoMedico,
                NombrePaciente = model.NombrePaciente,
                ApellidoPaciente = model.ApellidoPaciente,
                EspecialidadId = model.EspecialidadSeleccionada,
                SituacionConsulta = model.SituacionSeleccionada,
                FiltroFechaCita = model.FechaCita,
                Page = pager.CurrentPage,
                Rows = pager.RowsPerPage
            });

            ViewBag.Pager = pager;

            if (response.Success)
            {
                model.Consultas = response.Data;
                pager.TotalPages = response.TotalPaginas;
                pager.RowCount = response.Data.Count;
            }
            return View(model);
        }

        public async Task<IActionResult> Crear()
        {
            var vm = new FormConsultaViewModel
            {
                Especialidades = await _especialidadProxy.ListAsync(),
                Input = new ConsultaDTORequest
                {
                    FechaCita = DateTime.Today,
                    HoraCita  = DateTime.Today,
                    Situacion = SituacionConsulta.ConsultaPendiente, 
                }
            };
        

            return View(vm);
        }

    }


}
