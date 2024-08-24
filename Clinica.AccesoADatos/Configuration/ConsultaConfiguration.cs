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
    public class ConsultaConfiguration : IEntityTypeConfiguration<Consulta>
    {
        public void Configure(EntityTypeBuilder<Consulta> builder)
        {
            builder.Property(p => p.FechaCita)
                .HasColumnType("DATE")
                .IsRequired();
            builder.Property(p => p.HoraCita)
                .HasColumnType("DATETIME");

            
        }
    }
}
