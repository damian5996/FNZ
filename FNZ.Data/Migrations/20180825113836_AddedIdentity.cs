using Microsoft.EntityFrameworkCore.Migrations;

namespace FNZ.Data.Migrations
{
    public partial class AddedIdentity : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "LastEditedById",
                table: "Tabs",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ModeratorId",
                table: "Tabs",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ModeratorId",
                table: "Requests",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "EditedById",
                table: "Posts",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Moderators",
                columns: table => new
                {
                    UserName = table.Column<string>(nullable: true),
                    Email = table.Column<string>(nullable: true),
                    PasswordHash = table.Column<string>(nullable: true),
                    SecurityStamp = table.Column<string>(nullable: true),
                    ConcurrencyStamp = table.Column<string>(nullable: true),
                    Id = table.Column<string>(nullable: false),
                    IsAdmin = table.Column<bool>(nullable: false),
                    IsDeleted = table.Column<bool>(nullable: false),
                    FirstName = table.Column<string>(nullable: true),
                    LastName = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Moderators", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Tabs_LastEditedById",
                table: "Tabs",
                column: "LastEditedById");

            migrationBuilder.CreateIndex(
                name: "IX_Tabs_ModeratorId",
                table: "Tabs",
                column: "ModeratorId");

            migrationBuilder.CreateIndex(
                name: "IX_Requests_ModeratorId",
                table: "Requests",
                column: "ModeratorId");

            migrationBuilder.CreateIndex(
                name: "IX_Posts_EditedById",
                table: "Posts",
                column: "EditedById");

            migrationBuilder.AddForeignKey(
                name: "FK_Posts_Moderators_EditedById",
                table: "Posts",
                column: "EditedById",
                principalTable: "Moderators",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Requests_Moderators_ModeratorId",
                table: "Requests",
                column: "ModeratorId",
                principalTable: "Moderators",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Tabs_Moderators_LastEditedById",
                table: "Tabs",
                column: "LastEditedById",
                principalTable: "Moderators",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Tabs_Moderators_ModeratorId",
                table: "Tabs",
                column: "ModeratorId",
                principalTable: "Moderators",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Posts_Moderators_EditedById",
                table: "Posts");

            migrationBuilder.DropForeignKey(
                name: "FK_Requests_Moderators_ModeratorId",
                table: "Requests");

            migrationBuilder.DropForeignKey(
                name: "FK_Tabs_Moderators_LastEditedById",
                table: "Tabs");

            migrationBuilder.DropForeignKey(
                name: "FK_Tabs_Moderators_ModeratorId",
                table: "Tabs");

            migrationBuilder.DropTable(
                name: "Moderators");

            migrationBuilder.DropIndex(
                name: "IX_Tabs_LastEditedById",
                table: "Tabs");

            migrationBuilder.DropIndex(
                name: "IX_Tabs_ModeratorId",
                table: "Tabs");

            migrationBuilder.DropIndex(
                name: "IX_Requests_ModeratorId",
                table: "Requests");

            migrationBuilder.DropIndex(
                name: "IX_Posts_EditedById",
                table: "Posts");

            migrationBuilder.DropColumn(
                name: "LastEditedById",
                table: "Tabs");

            migrationBuilder.DropColumn(
                name: "ModeratorId",
                table: "Tabs");

            migrationBuilder.DropColumn(
                name: "ModeratorId",
                table: "Requests");

            migrationBuilder.DropColumn(
                name: "EditedById",
                table: "Posts");
        }
    }
}
