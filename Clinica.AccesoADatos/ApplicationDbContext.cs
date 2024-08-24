using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clinica.AccesoADatos
{
    public class ApplicationDbContext : IdentityDbContext<ClinicaIdentityUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext>options)
            :base(options)
        {
            
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);


            // AspNetUser RenombrarTabla
            builder.Entity<ClinicaIdentityUser>(e =>
            {
                e.ToTable("Usuario");
            });

            //AspRoles Renombrar
            builder.Entity<IdentityRole>(e =>
            {
                e.ToTable("Rol");
            });

            //AspUserRoles
            builder.Entity<IdentityUserRole<string>>(e =>
            {
                e.ToTable("UsuarioRol");
            });


        }
    }
}
