using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Meets.Migrations
{
    /// <inheritdoc />
    public partial class OptimisticConcurrency : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ConcurrencyStamp",
                table: "Meeting");

            migrationBuilder.AddColumn<byte[]>(
                name: "Version",
                table: "Meeting",
                type: "rowversion",
                rowVersion: true,
                nullable: false,
                defaultValue: new byte[0]);

            migrationBuilder.AddColumn<byte[]>(
                name: "Version",
                table: "AspNetUsers",
                type: "rowversion",
                rowVersion: true,
                nullable: false,
                defaultValue: new byte[0]);

            migrationBuilder.AddColumn<byte[]>(
                name: "Version",
                table: "ApplicationUserMeeting",
                type: "rowversion",
                rowVersion: true,
                nullable: false,
                defaultValue: new byte[0]);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Version",
                table: "Meeting");

            migrationBuilder.DropColumn(
                name: "Version",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "Version",
                table: "ApplicationUserMeeting");

            migrationBuilder.AddColumn<string>(
                name: "ConcurrencyStamp",
                table: "Meeting",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }
    }
}
