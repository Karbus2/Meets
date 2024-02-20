using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Meets.Migrations
{
    /// <inheritdoc />
    public partial class FriendshipSplitToInvitationsAndFriends : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Friendships");

            migrationBuilder.RenameColumn(
                name: "CreationDate",
                table: "Meetings",
                newName: "CreatedDate");

            migrationBuilder.RenameColumn(
                name: "CreationDate",
                table: "ApplicationUserMeetings",
                newName: "CreatedDate");

            migrationBuilder.CreateTable(
                name: "Friends",
                columns: table => new
                {
                    ApplicationUserId = table.Column<string>(type: "nvarchar(450)", maxLength: 450, nullable: false),
                    FriendId = table.Column<string>(type: "nvarchar(450)", maxLength: 450, nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETDATE()"),
                    Version = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Friends", x => new { x.ApplicationUserId, x.FriendId });
                    table.ForeignKey(
                        name: "FK_Friends_AspNetUsers_ApplicationUserId",
                        column: x => x.ApplicationUserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Invitations",
                columns: table => new
                {
                    RequesterId = table.Column<string>(type: "nvarchar(450)", maxLength: 450, nullable: false),
                    AddresseeId = table.Column<string>(type: "nvarchar(450)", maxLength: 450, nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETDATE()"),
                    Version = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Invitations", x => new { x.RequesterId, x.AddresseeId });
                    table.ForeignKey(
                        name: "FK_Invitations_AspNetUsers_AddresseeId",
                        column: x => x.AddresseeId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Invitations_AspNetUsers_RequesterId",
                        column: x => x.RequesterId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                });

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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Friends");

            migrationBuilder.DropTable(
                name: "Invitations");

            migrationBuilder.RenameColumn(
                name: "CreatedDate",
                table: "Meetings",
                newName: "CreationDate");

            migrationBuilder.RenameColumn(
                name: "CreatedDate",
                table: "ApplicationUserMeetings",
                newName: "CreationDate");

            migrationBuilder.CreateTable(
                name: "Friendships",
                columns: table => new
                {
                    AddresseeId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    RequesterId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Accepted = table.Column<bool>(type: "bit", nullable: false),
                    ApplicationUserId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    EndDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    StartDate = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETDATE()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Friendships", x => new { x.AddresseeId, x.RequesterId });
                    table.ForeignKey(
                        name: "FK_Friendships_AspNetUsers_AddresseeId",
                        column: x => x.AddresseeId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Friendships_AspNetUsers_ApplicationUserId",
                        column: x => x.ApplicationUserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Friendships_AspNetUsers_RequesterId",
                        column: x => x.RequesterId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Friendships_AddresseeId",
                table: "Friendships",
                column: "AddresseeId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Friendships_ApplicationUserId",
                table: "Friendships",
                column: "ApplicationUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Friendships_RequesterId",
                table: "Friendships",
                column: "RequesterId",
                unique: true);
        }
    }
}
