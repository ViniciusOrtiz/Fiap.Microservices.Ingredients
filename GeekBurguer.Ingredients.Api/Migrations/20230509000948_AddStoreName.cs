using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GeekBurguer.Ingredients.Api.Migrations
{
    /// <inheritdoc />
    public partial class AddStoreName : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "StoreName",
                table: "Products",
                type: "TEXT",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "StoreName",
                table: "Products");
        }
    }
}
