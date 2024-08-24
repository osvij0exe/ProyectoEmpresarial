using Clinica.AccesoADatos.JsonSettings;
using Clinica.Entities;
using Clinica.Repositories.Implementacion.AdoRepository.AdoRepositoryPatron;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clinica.Repositories.Implementacion.AdoRepository.AdoRepository
{
    public class AdoMedicoRepositoryPatron : AdoRepositoryBase<Medico>
    {
        public AdoMedicoRepositoryPatron(IOptions<ConnectionSetting> connection)
            : base(connection)
        {

        }
    }
}
