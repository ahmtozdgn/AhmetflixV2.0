using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Ahmetflix.Migrations
{
    /// <inheritdoc />
    public partial class MovieSeedAdded : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "AddressId",
                table: "Movies",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Movies_AddressId",
                table: "Movies",
                column: "AddressId");

            migrationBuilder.AddForeignKey(
                name: "FK_Movies_Addresses_AddressId",
                table: "Movies",
                column: "AddressId",
                principalTable: "Addresses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Movies_Addresses_AddressId",
                table: "Movies");

            migrationBuilder.DropIndex(
                name: "IX_Movies_AddressId",
                table: "Movies");

            migrationBuilder.DropColumn(
                name: "AddressId",
                table: "Movies");
        }
    }
}
