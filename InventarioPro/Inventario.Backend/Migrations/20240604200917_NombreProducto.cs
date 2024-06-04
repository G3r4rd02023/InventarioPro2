using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Inventario.Backend.Migrations
{
    /// <inheritdoc />
    public partial class NombreProducto : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Producto",
                table: "Operaciones",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Producto",
                table: "Operaciones");
        }
    }
}
