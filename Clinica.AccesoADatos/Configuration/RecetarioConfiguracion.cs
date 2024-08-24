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
    public class RecetarioConfiguracion : IEntityTypeConfiguration<Recetario>
    {
        public void Configure(EntityTypeBuilder<Recetario> builder)
        {
            builder.Property(p => p.Prescripcion)
                .IsRequired()
                .HasMaxLength(1000);
        }
    }
}
