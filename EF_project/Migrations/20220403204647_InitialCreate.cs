using Microsoft.EntityFrameworkCore.Migrations;

namespace EF_project.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Users_Chats_ChatEntityName",
                table: "Users");

            migrationBuilder.DropIndex(
                name: "IX_Users_ChatEntityName",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "ChatEntityName",
                table: "Users");

            migrationBuilder.CreateTable(
                name: "ChatEntityUserEntity",
                columns: table => new
                {
                    ParticipantsNickName = table.Column<string>(type: "TEXT", nullable: false),
                    UserChatsName = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ChatEntityUserEntity", x => new { x.ParticipantsNickName, x.UserChatsName });
                    table.ForeignKey(
                        name: "FK_ChatEntityUserEntity_Chats_UserChatsName",
                        column: x => x.UserChatsName,
                        principalTable: "Chats",
                        principalColumn: "Name",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ChatEntityUserEntity_Users_ParticipantsNickName",
                        column: x => x.ParticipantsNickName,
                        principalTable: "Users",
                        principalColumn: "NickName",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ChatEntityUserEntity_UserChatsName",
                table: "ChatEntityUserEntity",
                column: "UserChatsName");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ChatEntityUserEntity");

            migrationBuilder.AddColumn<string>(
                name: "ChatEntityName",
                table: "Users",
                type: "TEXT",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Users_ChatEntityName",
                table: "Users",
                column: "ChatEntityName");

            migrationBuilder.AddForeignKey(
                name: "FK_Users_Chats_ChatEntityName",
                table: "Users",
                column: "ChatEntityName",
                principalTable: "Chats",
                principalColumn: "Name",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
