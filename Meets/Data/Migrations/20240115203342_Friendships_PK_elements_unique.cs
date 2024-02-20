using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Meets.Migrations
{
    /// <inheritdoc />
    public partial class Friendships_PK_elements_unique : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Friendships_RequesterId",
                table: "Friendships");

            migrationBuilder.CreateIndex(
                name: "IX_Friendships_AddresseeId",
                table: "Friendships",
                column: "AddresseeId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Friendships_RequesterId",
                table: "Friendships",
                column: "RequesterId",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Friendships_AddresseeId",
                table: "Friendships");

            migrationBuilder.DropIndex(
                name: "IX_Friendships_RequesterId",
                table: "Friendships");

            migrationBuilder.CreateIndex(
                name: "IX_Friendships_RequesterId",
                table: "Friendships",
                column: "RequesterId");
        }
    }
}
