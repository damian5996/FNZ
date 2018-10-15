using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace FNZ.Data.Migrations
{
    public partial class DeletedModeratorsTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
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

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "LastEditedById",
                table: "Tabs",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ModeratorId",
                table: "Tabs",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ModeratorId",
                table: "Requests",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "EditedById",
                table: "Posts",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Moderators",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Email = table.Column<string>(nullable: true),
                    FirstName = table.Column<string>(nullable: true),
                    IsAdmin = table.Column<bool>(nullable: false),
                    IsDeleted = table.Column<bool>(nullable: false),
                    LastName = table.Column<string>(nullable: true),
                    PasswordHash = table.Column<string>(nullable: true)
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
    }
}
