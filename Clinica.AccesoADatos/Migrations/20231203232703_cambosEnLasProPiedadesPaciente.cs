using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Clinica.AccesoADatos.Migrations
{
    /// <inheritdoc />
    public partial class cambosEnLasProPiedadesPaciente : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<float>(
                name: "Temperatura",
                table: "Paciente",
                type: "real",
                maxLength: 100,
                nullable: false,
                oldClrType: typeof(double),
                oldType: "float",
                oldMaxLength: 100);

            migrationBuilder.AlterColumn<float>(
                name: "Talla",
                table: "Paciente",
                type: "real",
                maxLength: 800,
                nullable: false,
                oldClrType: typeof(double),
                oldType: "float",
                oldMaxLength: 800);

            migrationBuilder.AlterColumn<float>(
                name: "Peso",
                table: "Paciente",
                type: "real",
                maxLength: 900,
                nullable: false,
                oldClrType: typeof(double),
                oldType: "float",
                oldMaxLength: 900);

            migrationBuilder.AlterColumn<float>(
                name: "IMC",
                table: "Paciente",
                type: "real",
                maxLength: 1000,
                nullable: false,
                oldClrType: typeof(double),
                oldType: "float",
                oldMaxLength: 1000);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<double>(
                name: "Temperatura",
                table: "Paciente",
                type: "float",
                maxLength: 100,
                nullable: false,
                oldClrType: typeof(float),
                oldType: "real",
                oldMaxLength: 100);

            migrationBuilder.AlterColumn<double>(
                name: "Talla",
                table: "Paciente",
                type: "float",
                maxLength: 800,
                nullable: false,
                oldClrType: typeof(float),
                oldType: "real",
                oldMaxLength: 800);

            migrationBuilder.AlterColumn<double>(
                name: "Peso",
                table: "Paciente",
                type: "float",
                maxLength: 900,
                nullable: false,
                oldClrType: typeof(float),
                oldType: "real",
                oldMaxLength: 900);

            migrationBuilder.AlterColumn<double>(
                name: "IMC",
                table: "Paciente",
                type: "float",
                maxLength: 1000,
                nullable: false,
                oldClrType: typeof(float),
                oldType: "real",
                oldMaxLength: 1000);
        }
    }
}
