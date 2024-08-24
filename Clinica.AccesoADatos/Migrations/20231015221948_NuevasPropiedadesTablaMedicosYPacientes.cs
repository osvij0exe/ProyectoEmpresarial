using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Clinica.AccesoADatos.Migrations
{
    /// <inheritdoc />
    public partial class NuevasPropiedadesTablaMedicosYPacientes : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Edad",
                table: "Paciente",
                type: "int",
                maxLength: 100,
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "CedulaProfecional",
                table: "Medico",
                type: "int",
                maxLength: 20,
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "Correo",
                table: "Medico",
                type: "varchar(max)",
                unicode: false,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Genero",
                table: "Medico",
                type: "nvarchar(1)",
                maxLength: 1,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Telefono",
                table: "Medico",
                type: "nvarchar(25)",
                maxLength: 25,
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Edad",
                table: "Paciente");

            migrationBuilder.DropColumn(
                name: "CedulaProfecional",
                table: "Medico");

            migrationBuilder.DropColumn(
                name: "Correo",
                table: "Medico");

            migrationBuilder.DropColumn(
                name: "Genero",
                table: "Medico");

            migrationBuilder.DropColumn(
                name: "Telefono",
                table: "Medico");
        }
    }
}
