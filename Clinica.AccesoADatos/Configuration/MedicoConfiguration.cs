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
    public class MedicoConfiguration : IEntityTypeConfiguration<Medico>
    {
        public void Configure(EntityTypeBuilder<Medico> builder)
        {
            builder.Property(p => p.Nombres)
                .HasMaxLength(50)
                .IsRequired();
            builder.Property(p => p.Apellidos)
                .HasMaxLength(50)
                .IsRequired();
            builder.HasQueryFilter(p => p.Estado);
            builder.Property(p => p.CedulaProfecional)
                .HasMaxLength(20);
            builder.Property(p => p.Correo)
                .IsUnicode(false);
            builder.Property(p => p.Telefono)
                .HasMaxLength(25);
            builder.Property(p => p.Genero) 
                .HasMaxLength(1)
                .IsRequired();
        }
    }
}
