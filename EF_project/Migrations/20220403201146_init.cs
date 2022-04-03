using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace EF_project.Migrations
{
    public partial class init : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Chats",
                columns: table => new
                {
                    Name = table.Column<string>(type: "TEXT", nullable: false),
                    DateCreated = table.Column<DateTime>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Chats", x => x.Name);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    NickName = table.Column<string>(type: "TEXT", nullable: false),
                    Email = table.Column<string>(type: "TEXT", nullable: true),
                    ChatEntityName = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.NickName);
                    table.ForeignKey(
                        name: "FK_Users_Chats_ChatEntityName",
                        column: x => x.ChatEntityName,
                        principalTable: "Chats",
                        principalColumn: "Name",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Messages",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    DateSent = table.Column<DateTime>(type: "TEXT", nullable: false),
                    Contents = table.Column<string>(type: "TEXT", nullable: true),
                    AuthorNickName = table.Column<string>(type: "TEXT", nullable: true),
                    ChatName = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Messages", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Messages_Chats_ChatName",
                        column: x => x.ChatName,
                        principalTable: "Chats",
                        principalColumn: "Name",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Messages_Users_AuthorNickName",
                        column: x => x.AuthorNickName,
                        principalTable: "Users",
                        principalColumn: "NickName",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Messages_AuthorNickName",
                table: "Messages",
                column: "AuthorNickName");

            migrationBuilder.CreateIndex(
                name: "IX_Messages_ChatName",
                table: "Messages",
                column: "ChatName");

            migrationBuilder.CreateIndex(
                name: "IX_Users_ChatEntityName",
                table: "Users",
                column: "ChatEntityName");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Messages");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "Chats");
        }
    }
}
