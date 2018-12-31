using Microsoft.EntityFrameworkCore.Migrations;

namespace FNZ.Data.Migrations
{
    public partial class ChangedRelationsBetweenAnimalAndPost : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Animals_Posts_PostId",
                table: "Animals");

            migrationBuilder.DropIndex(
                name: "IX_Animals_PostId",
                table: "Animals");

            migrationBuilder.DropColumn(
                name: "PostId",
                table: "Animals");

            migrationBuilder.AddColumn<long>(
                name: "AnimalId",
                table: "Posts",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Posts_AnimalId",
                table: "Posts",
                column: "AnimalId");

            migrationBuilder.AddForeignKey(
                name: "FK_Posts_Animals_AnimalId",
                table: "Posts",
                column: "AnimalId",
                principalTable: "Animals",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Posts_Animals_AnimalId",
                table: "Posts");

            migrationBuilder.DropIndex(
                name: "IX_Posts_AnimalId",
                table: "Posts");

            migrationBuilder.DropColumn(
                name: "AnimalId",
                table: "Posts");

            migrationBuilder.AddColumn<long>(
                name: "PostId",
                table: "Animals",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Animals_PostId",
                table: "Animals",
                column: "PostId");

            migrationBuilder.AddForeignKey(
                name: "FK_Animals_Posts_PostId",
                table: "Animals",
                column: "PostId",
                principalTable: "Posts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
