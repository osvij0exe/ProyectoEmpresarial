using Clinica.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clinica.AccesoADatos.Configuration
{
    public class EspecialidadConfiguración : IEntityTypeConfiguration<Especialidad>
    {
        public void Configure(EntityTypeBuilder<Especialidad> builder)
        {
            builder.Property(p => p.NombreEspecialidad)
                .IsRequired()
                .HasMaxLength(30);

            var fecha = DateTime.Parse("2023-08-01");

            builder.HasData(new List<Especialidad> {
            new() { Id = 1, NombreEspecialidad = "Cardiologia", FechaCreacion  = fecha},
            new() { Id = 2, NombreEspecialidad = "Oftalmologia", FechaCreacion = fecha },
            new() { Id = 3, NombreEspecialidad = "Dermatologia", FechaCreacion = fecha },
            new() { Id = 4, NombreEspecialidad = "Otorrinolaringologia", FechaCreacion = fecha },
            new() { Id = 5, NombreEspecialidad = "Gastroenterologia", FechaCreacion = fecha }

            });

        }
    }
}
