using AutoMapper;
using Clinica.Entities;
using Clinica.Models.Request;
using Clinica.Models.Response;
using Clinica.Models.Response.MedicoResponse;
using Clinica.Models.Response.PacienteResponse;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clinica.Services.Profiles
{
    public class Profiles: Profile
    {
        public Profiles()
        {
            //CreateMap<Fuente,Destino>();
            //Medico
            CreateMap<Medico, MedicoDTOResponse>();
            CreateMap<MedicoDTORequest, Medico>();
            CreateMap<Medico, MedicoConsultaDTO>();
            //ado.net
            CreateMap<Medico,MedicoAdoDTOResponse>();


            //Paciente
            CreateMap<Paciente, PacienteDTOResponse>();
            CreateMap<PacienteDTORequest, Paciente>();
            CreateMap<Paciente, PacienteConsultaDTO>();
            CreateMap<PacienteDTOPatch, Paciente>();


            //Consultas
            CreateMap<Consulta, ConsultaDTOResponse>()
                .ForMember(destino => destino.FechaCita, origen => origen.MapFrom(x => $"{x.FechaCita:dd/MM/yyyy} {x.HoraCita:hh:mm tt}"))
                .ForMember(destino => destino.HoraCita, origen => origen.MapFrom(x => $"{x.HoraCita:hh:mm tt}"));
            CreateMap<ConsultaDTORequest, Consulta>();

            //Especialidad
            CreateMap<Especialidad, EspecialidadDTOResponse>();

            //Recetario

            CreateMap<Recetario, RecetarioDTOResponse>();
            CreateMap<RecetarioDTORequest, Recetario>();

            

        }
    }
}
