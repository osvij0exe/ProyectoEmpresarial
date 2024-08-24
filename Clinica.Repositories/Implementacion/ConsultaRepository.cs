using Clinica.AccesoADatos;
using Clinica.Entities;
using Clinica.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clinica.Repositories.Implementacion
{
    public class ConsultaRepository : RepositoryBase<Consulta>, IConsultaRepository
    {
        public ConsultaRepository(ClinicaDbContext context)
            : base(context)
        {
            
        }

    }
}
