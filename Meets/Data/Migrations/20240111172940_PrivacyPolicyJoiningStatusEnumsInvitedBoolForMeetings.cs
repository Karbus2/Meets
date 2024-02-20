using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Meets.Migrations
{
    /// <inheritdoc />
    public partial class PrivacyPolicyJoiningStatusEnumsInvitedBoolForMeetings : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Location",
                table: "Meetings",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "MaxSize",
                table: "Meetings",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "PrivacyPolicy",
                table: "Meetings",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<bool>(
                name: "Invited",
                table: "ApplicationUserMeetings",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "JoiningStatus",
                table: "ApplicationUserMeetings",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Location",
                table: "Meetings");

            migrationBuilder.DropColumn(
                name: "MaxSize",
                table: "Meetings");

            migrationBuilder.DropColumn(
                name: "PrivacyPolicy",
                table: "Meetings");

            migrationBuilder.DropColumn(
                name: "Invited",
                table: "ApplicationUserMeetings");

            migrationBuilder.DropColumn(
                name: "JoiningStatus",
                table: "ApplicationUserMeetings");
        }
    }
}
