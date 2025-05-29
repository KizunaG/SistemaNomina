using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace NominaSystem.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AgregarRelacionesDetalleDescuentoNomina : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "DescuentoId",
                table: "DetallesDescuentoNomina",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "NominaId",
                table: "DetallesDescuentoNomina",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Nominas_ID_Empleado",
                table: "Nominas",
                column: "ID_Empleado");

            migrationBuilder.CreateIndex(
                name: "IX_DetallesDescuentoNomina_DescuentoId",
                table: "DetallesDescuentoNomina",
                column: "DescuentoId");

            migrationBuilder.CreateIndex(
                name: "IX_DetallesDescuentoNomina_NominaId",
                table: "DetallesDescuentoNomina",
                column: "NominaId");

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

            migrationBuilder.AddForeignKey(
                name: "FK_Nominas_Empleados_ID_Empleado",
                table: "Nominas",
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
                name: "FK_Nominas_Empleados_ID_Empleado",
                table: "Nominas");

            migrationBuilder.DropIndex(
                name: "IX_Nominas_ID_Empleado",
                table: "Nominas");

            migrationBuilder.DropIndex(
                name: "IX_DetallesDescuentoNomina_DescuentoId",
                table: "DetallesDescuentoNomina");

            migrationBuilder.DropIndex(
                name: "IX_DetallesDescuentoNomina_NominaId",
                table: "DetallesDescuentoNomina");

            migrationBuilder.DropColumn(
                name: "DescuentoId",
                table: "DetallesDescuentoNomina");

            migrationBuilder.DropColumn(
                name: "NominaId",
                table: "DetallesDescuentoNomina");
        }
    }
}
