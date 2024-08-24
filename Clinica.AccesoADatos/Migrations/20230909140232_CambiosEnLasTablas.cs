using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Clinica.AccesoADatos.Migrations
{
    /// <inheritdoc />
    public partial class CambiosEnLasTablas : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Medico_Expediente_ExpedienteId",
                table: "Medico");

            migrationBuilder.DropIndex(
                name: "IX_Medico_ExpedienteId",
                table: "Medico");

            migrationBuilder.DropColumn(
                name: "Especialidad",
                table: "Medico");

            migrationBuilder.DropColumn(
                name: "ExpedienteId",
                table: "Medico");

            migrationBuilder.AddColumn<int>(
                name: "Medicos",
                table: "Expediente",
                type: "int",
                nullable: false,
                defaultValue: 0)
                .Annotation("SqlServer:IsTemporal", true)
                .Annotation("SqlServer:TemporalHistoryTableName", "ExpedienteHistory")
                .Annotation("SqlServer:TemporalHistoryTableSchema", null)
                .Annotation("SqlServer:TemporalPeriodEndColumnName", "PeriodEnd")
                .Annotation("SqlServer:TemporalPeriodStartColumnName", "PeriodStart");

            migrationBuilder.AddColumn<int>(
                name: "EspecialidadId",
                table: "Consulta",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "Especialidad",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NombreEspecialidad = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    Estado = table.Column<bool>(type: "bit", nullable: false),
                    FechaCreacion = table.Column<DateTime>(type: "datetime2", nullable: false),
                    FechaModificacion = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Especialidad", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "Especialidad",
                columns: new[] { "Id", "Estado", "FechaCreacion", "FechaModificacion", "NombreEspecialidad" },
                values: new object[,]
                {
                    { 1, true, new DateTime(2023, 8, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "Cardiologia" },
                    { 2, true, new DateTime(2023, 8, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "Oftalmologia" },
                    { 3, true, new DateTime(2023, 8, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "Dermatologia" },
                    { 4, true, new DateTime(2023, 8, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "Otorrinolaringologia" },
                    { 5, true, new DateTime(2023, 8, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "Gastroenterologia" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Medico_EspecialidadId",
                table: "Medico",
                column: "EspecialidadId");

            migrationBuilder.CreateIndex(
                name: "IX_Consulta_EspecialidadId",
                table: "Consulta",
                column: "EspecialidadId");

            migrationBuilder.AddForeignKey(
                name: "FK_Consulta_Especialidad_EspecialidadId",
                table: "Consulta",
                column: "EspecialidadId",
                principalTable: "Especialidad",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Medico_Especialidad_EspecialidadId",
                table: "Medico",
                column: "EspecialidadId",
                principalTable: "Especialidad",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Consulta_Especialidad_EspecialidadId",
                table: "Consulta");

            migrationBuilder.DropForeignKey(
                name: "FK_Medico_Especialidad_EspecialidadId",
                table: "Medico");

            migrationBuilder.DropTable(
                name: "Especialidad");

            migrationBuilder.DropIndex(
                name: "IX_Medico_EspecialidadId",
                table: "Medico");

            migrationBuilder.DropIndex(
                name: "IX_Consulta_EspecialidadId",
                table: "Consulta");

            migrationBuilder.DropColumn(
                name: "Medicos",
                table: "Expediente")
                .Annotation("SqlServer:IsTemporal", true)
                .Annotation("SqlServer:TemporalHistoryTableName", "ExpedienteHistory")
                .Annotation("SqlServer:TemporalHistoryTableSchema", null)
                .Annotation("SqlServer:TemporalPeriodEndColumnName", "PeriodEnd")
                .Annotation("SqlServer:TemporalPeriodStartColumnName", "PeriodStart");

            migrationBuilder.DropColumn(
                name: "EspecialidadId",
                table: "Consulta");

            migrationBuilder.AddColumn<int>(
                name: "Especialidad",
                table: "Medico",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "ExpedienteId",
                table: "Medico",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Medico_ExpedienteId",
                table: "Medico",
                column: "ExpedienteId");

            migrationBuilder.AddForeignKey(
                name: "FK_Medico_Expediente_ExpedienteId",
                table: "Medico",
                column: "ExpedienteId",
                principalTable: "Expediente",
                principalColumn: "Id");
        }
    }
}
