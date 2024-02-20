using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Meets.Migrations
{
    /// <inheritdoc />
    public partial class InvIsNotUnique : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Invitations_AddresseeId",
                table: "Invitations");

            migrationBuilder.DropIndex(
                name: "IX_Invitations_RequesterId",
                table: "Invitations");

            migrationBuilder.CreateIndex(
                name: "IX_Invitations_AddresseeId",
                table: "Invitations",
                column: "AddresseeId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Invitations_AddresseeId",
                table: "Invitations");

            migrationBuilder.CreateIndex(
                name: "IX_Invitations_AddresseeId",
                table: "Invitations",
                column: "AddresseeId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Invitations_RequesterId",
                table: "Invitations",
                column: "RequesterId",
                unique: true);
        }
    }
}
