using Clinica.Common;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Clinica.AccesoADatos
{
    public static class UserDataSeeder
    {
        public static async Task Seed(IServiceProvider service)
        {
            // Repositorio de Usuarios
            var userManager = service.GetRequiredService<UserManager<ClinicaIdentityUser>>();

            // Repositorio de  Rolers
            var roleManager = service.GetRequiredService<RoleManager<IdentityRole>>();

            // Crear Roles
            var adminRole = new IdentityRole(Constantes.RolAdmin);

            var PacienteRol = new IdentityRole(Constantes.RolPaciente);

            var medicoRol = new IdentityRole(Constantes.RolMedico);

            if (!await roleManager.RoleExistsAsync(Constantes.RolAdmin))
                await roleManager.CreateAsync(adminRole);

            if (!await roleManager.RoleExistsAsync(Constantes.RolPaciente))
                await roleManager.CreateAsync(PacienteRol);

            if (!await roleManager.RoleExistsAsync(Constantes.RolMedico))
                await roleManager.CreateAsync(medicoRol);

            // Creando el usuario Admin
            var adminUser = new ClinicaIdentityUser
            {
                Nombre   = "Administrador",
                Apellido = "Del Sistema",
                UserName = "admin",
                Email    = "admin@gmail.com",
                PhoneNumber    = "+1 999 999 999",
                EmailConfirmed = true,
            };

            var result = await userManager.CreateAsync(adminUser, "Admin1234*");
            if(result.Succeeded)
            {
                //Esto me asegura que el usuario se creó correctamente
                adminUser = await userManager.FindByEmailAsync(adminUser.Email);
                if(adminUser is not null)
                    await userManager.AddToRoleAsync(adminUser, Constantes.RolAdmin);

            }






        }



    }
}
