using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace Clinica.AccesoADatos
{
    public class ClinicaDbContext : DbContext
    {
        public ClinicaDbContext(DbContextOptions<ClinicaDbContext> options)
            :base(options)
        {


            
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            //Se agrega la configuracion de las entidades desde este mismo ensamblado
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());


        }
    }
}