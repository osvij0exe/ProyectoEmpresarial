using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Clinica.AccesoADatos.Migrations
{
    /// <inheritdoc />
    public partial class NuevasPropiedadesTblaPacientesYTablaRecetarioAgregada : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ExpedienteId",
                table: "Paciente");

            migrationBuilder.AddColumn<string>(
                name: "Alergias",
                table: "Paciente",
                type: "nvarchar(500)",
                maxLength: 500,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "FactorReumatoideo",
                table: "Paciente",
                type: "int",
                maxLength: 300,
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "FrecuenciaCardiaca",
                table: "Paciente",
                type: "int",
                maxLength: 300,
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<double>(
                name: "IMC",
                table: "Paciente",
                type: "float",
                maxLength: 1000,
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "Peso",
                table: "Paciente",
                type: "float",
                maxLength: 900,
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "Talla",
                table: "Paciente",
                type: "float",
                maxLength: 800,
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "Temperatura",
                table: "Paciente",
                type: "float",
                maxLength: 100,
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<int>(
                name: "TensionArterial",
                table: "Paciente",
                type: "int",
                maxLength: 300,
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "Recetario",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Prescripcion = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: false),
                    PacienteId = table.Column<int>(type: "int", nullable: false),
                    MedicoId = table.Column<int>(type: "int", nullable: false),
                    Estado = table.Column<bool>(type: "bit", nullable: false),
                    FechaCreacion = table.Column<DateTime>(type: "datetime2", nullable: false),
                    FechaModificacion = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Recetario", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Recetario_Medico_MedicoId",
                        column: x => x.MedicoId,
                        principalTable: "Medico",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Recetario_Paciente_PacienteId",
                        column: x => x.PacienteId,
                        principalTable: "Paciente",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Recetario_MedicoId",
                table: "Recetario",
                column: "MedicoId");

            migrationBuilder.CreateIndex(
                name: "IX_Recetario_PacienteId",
                table: "Recetario",
                column: "PacienteId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Recetario");

            migrationBuilder.DropColumn(
                name: "Alergias",
                table: "Paciente");

            migrationBuilder.DropColumn(
                name: "FactorReumatoideo",
                table: "Paciente");

            migrationBuilder.DropColumn(
                name: "FrecuenciaCardiaca",
                table: "Paciente");

            migrationBuilder.DropColumn(
                name: "IMC",
                table: "Paciente");

            migrationBuilder.DropColumn(
                name: "Peso",
                table: "Paciente");

            migrationBuilder.DropColumn(
                name: "Talla",
                table: "Paciente");

            migrationBuilder.DropColumn(
                name: "Temperatura",
                table: "Paciente");

            migrationBuilder.DropColumn(
                name: "TensionArterial",
                table: "Paciente");

            migrationBuilder.AddColumn<int>(
                name: "ExpedienteId",
                table: "Paciente",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
