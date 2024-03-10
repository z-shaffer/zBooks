using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace zBooksWeb.Migrations
{
    /// <inheritdoc />
    public partial class CategoriesTypeRenamedCategories : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_CategoriesType",
                table: "CategoriesType");

            migrationBuilder.RenameTable(
                name: "CategoriesType",
                newName: "Categories");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Categories",
                table: "Categories",
                column: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_Categories",
                table: "Categories");

            migrationBuilder.RenameTable(
                name: "Categories",
                newName: "CategoriesType");

            migrationBuilder.AddPrimaryKey(
                name: "PK_CategoriesType",
                table: "CategoriesType",
                column: "Id");
        }
    }
}
