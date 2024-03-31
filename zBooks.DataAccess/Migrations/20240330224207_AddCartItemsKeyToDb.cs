using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace zBooks.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class AddCartItemsKeyToDb : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "CartId",
                table: "CartItems",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CartId",
                table: "CartItems");
        }
    }
}
