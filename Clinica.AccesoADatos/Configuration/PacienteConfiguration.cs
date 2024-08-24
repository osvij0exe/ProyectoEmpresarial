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
    public class PacienteConfiguration : IEntityTypeConfiguration<Paciente>
    {
        public void Configure(EntityTypeBuilder<Paciente> builder)
        {
            builder.Property(p => p.Nombres)
                .HasMaxLength(50)
                .IsRequired();
            builder.Property(p => p.Apellidos)
                .HasMaxLength(50)
                .IsRequired();
            builder.Property(p => p.Estado)
                .HasMaxLength(20)
                .IsRequired();
            builder.Property(p => p.Provincia)
                .HasMaxLength(20)
                .IsRequired();
            builder.Property(p => p.Edad)
                .HasMaxLength(100);
            builder.Property(p => p.FechaNacimiento)
                .HasColumnType("DATE");
            builder.Property(p => p.Email)
                .HasMaxLength(200);
            builder.Property(p => p.Telefono)
                .HasMaxLength(200);
            builder.HasQueryFilter(p => p.Estado);
            builder.Property(p => p.TensionArterial)
                .HasMaxLength (300);
            builder.Property(p => p.FrecuenciaCardiaca)
                .HasMaxLength(300);
            builder.Property(p => p.FactorReumatoideo)
                .HasMaxLength(300);
            builder.Property(p => p.Temperatura)
                .HasMaxLength(100);
            builder.Property(p => p.Peso)
                .HasMaxLength(900);
            builder.Property(p => p.Talla)
                .HasMaxLength(800);
            builder.Property(p => p.IMC)
                .HasMaxLength(1000);
            builder.Property(p => p.Alergias)
                .HasMaxLength(500);

        }
    }
}
