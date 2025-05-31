using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace NominaSystem.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class InicialCreacion : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DetallesDescuentoNomina_DescuentosLegales_DescuentoId",
                table: "DetallesDescuentoNomina");

            migrationBuilder.DropForeignKey(
                name: "FK_DetallesDescuentoNomina_Nominas_NominaId",
                table: "DetallesDescuentoNomina");

            migrationBuilder.AddColumn<string>(
                name: "GradoAcademico",
                table: "InformacionAcademica",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Correo",
                table: "ExpedientesEmpleado",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Direccion",
                table: "ExpedientesEmpleado",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Dpi",
                table: "ExpedientesEmpleado",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "EstadoLaboral",
                table: "ExpedientesEmpleado",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<DateTime>(
                name: "FechaIngreso",
                table: "ExpedientesEmpleado",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "Nombre",
                table: "ExpedientesEmpleado",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Telefono",
                table: "ExpedientesEmpleado",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Nombre",
                table: "DocumentosEmpleado",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AlterColumn<int>(
                name: "NominaId",
                table: "DetallesDescuentoNomina",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "DescuentoId",
                table: "DetallesDescuentoNomina",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.CreateIndex(
                name: "IX_ExpedientesEmpleado_ID_Empleado",
                table: "ExpedientesEmpleado",
                column: "ID_Empleado");

            migrationBuilder.AddForeignKey(
                name: "FK_DetallesDescuentoNomina_DescuentosLegales_DescuentoId",
                table: "DetallesDescuentoNomina",
                column: "DescuentoId",
                principalTable: "DescuentosLegales",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_DetallesDescuentoNomina_Nominas_NominaId",
                table: "DetallesDescuentoNomina",
                column: "NominaId",
                principalTable: "Nominas",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ExpedientesEmpleado_Empleados_ID_Empleado",
                table: "ExpedientesEmpleado",
                column: "ID_Empleado",
                principalTable: "Empleados",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DetallesDescuentoNomina_DescuentosLegales_DescuentoId",
                table: "DetallesDescuentoNomina");

            migrationBuilder.DropForeignKey(
                name: "FK_DetallesDescuentoNomina_Nominas_NominaId",
                table: "DetallesDescuentoNomina");

            migrationBuilder.DropForeignKey(
                name: "FK_ExpedientesEmpleado_Empleados_ID_Empleado",
                table: "ExpedientesEmpleado");

            migrationBuilder.DropIndex(
                name: "IX_ExpedientesEmpleado_ID_Empleado",
                table: "ExpedientesEmpleado");

            migrationBuilder.DropColumn(
                name: "GradoAcademico",
                table: "InformacionAcademica");

            migrationBuilder.DropColumn(
                name: "Correo",
                table: "ExpedientesEmpleado");

            migrationBuilder.DropColumn(
                name: "Direccion",
                table: "ExpedientesEmpleado");

            migrationBuilder.DropColumn(
                name: "Dpi",
                table: "ExpedientesEmpleado");

            migrationBuilder.DropColumn(
                name: "EstadoLaboral",
                table: "ExpedientesEmpleado");

            migrationBuilder.DropColumn(
                name: "FechaIngreso",
                table: "ExpedientesEmpleado");

            migrationBuilder.DropColumn(
                name: "Nombre",
                table: "ExpedientesEmpleado");

            migrationBuilder.DropColumn(
                name: "Telefono",
                table: "ExpedientesEmpleado");

            migrationBuilder.DropColumn(
                name: "Nombre",
                table: "DocumentosEmpleado");

            migrationBuilder.AlterColumn<int>(
                name: "NominaId",
                table: "DetallesDescuentoNomina",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "DescuentoId",
                table: "DetallesDescuentoNomina",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_DetallesDescuentoNomina_DescuentosLegales_DescuentoId",
                table: "DetallesDescuentoNomina",
                column: "DescuentoId",
                principalTable: "DescuentosLegales",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_DetallesDescuentoNomina_Nominas_NominaId",
                table: "DetallesDescuentoNomina",
                column: "NominaId",
                principalTable: "Nominas",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
