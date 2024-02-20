using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Meets.Migrations
{
    /// <inheritdoc />
    public partial class CreateDbSetsMeetingAndAppUserMeeting : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ApplicationUserMeeting_AspNetUsers_ApplicationUserId",
                table: "ApplicationUserMeeting");

            migrationBuilder.DropForeignKey(
                name: "FK_ApplicationUserMeeting_Meeting_MeetingId",
                table: "ApplicationUserMeeting");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Meeting",
                table: "Meeting");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ApplicationUserMeeting",
                table: "ApplicationUserMeeting");

            migrationBuilder.RenameTable(
                name: "Meeting",
                newName: "Meetings");

            migrationBuilder.RenameTable(
                name: "ApplicationUserMeeting",
                newName: "ApplicationUserMeetings");

            migrationBuilder.RenameIndex(
                name: "IX_ApplicationUserMeeting_MeetingId",
                table: "ApplicationUserMeetings",
                newName: "IX_ApplicationUserMeetings_MeetingId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Meetings",
                table: "Meetings",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ApplicationUserMeetings",
                table: "ApplicationUserMeetings",
                columns: new[] { "ApplicationUserId", "MeetingId" });

            migrationBuilder.AddForeignKey(
                name: "FK_ApplicationUserMeetings_AspNetUsers_ApplicationUserId",
                table: "ApplicationUserMeetings",
                column: "ApplicationUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ApplicationUserMeetings_Meetings_MeetingId",
                table: "ApplicationUserMeetings",
                column: "MeetingId",
                principalTable: "Meetings",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ApplicationUserMeetings_AspNetUsers_ApplicationUserId",
                table: "ApplicationUserMeetings");

            migrationBuilder.DropForeignKey(
                name: "FK_ApplicationUserMeetings_Meetings_MeetingId",
                table: "ApplicationUserMeetings");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Meetings",
                table: "Meetings");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ApplicationUserMeetings",
                table: "ApplicationUserMeetings");

            migrationBuilder.RenameTable(
                name: "Meetings",
                newName: "Meeting");

            migrationBuilder.RenameTable(
                name: "ApplicationUserMeetings",
                newName: "ApplicationUserMeeting");

            migrationBuilder.RenameIndex(
                name: "IX_ApplicationUserMeetings_MeetingId",
                table: "ApplicationUserMeeting",
                newName: "IX_ApplicationUserMeeting_MeetingId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Meeting",
                table: "Meeting",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ApplicationUserMeeting",
                table: "ApplicationUserMeeting",
                columns: new[] { "ApplicationUserId", "MeetingId" });

            migrationBuilder.AddForeignKey(
                name: "FK_ApplicationUserMeeting_AspNetUsers_ApplicationUserId",
                table: "ApplicationUserMeeting",
                column: "ApplicationUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ApplicationUserMeeting_Meeting_MeetingId",
                table: "ApplicationUserMeeting",
                column: "MeetingId",
                principalTable: "Meeting",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
