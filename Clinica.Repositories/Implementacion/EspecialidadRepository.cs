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
    public class EspecialidadRepository : RepositoryBase<Especialidad>, IEspecialidadRepository
    {
        public EspecialidadRepository(ClinicaDbContext context)
            : base(context)
        {
            
        }

    }
}
