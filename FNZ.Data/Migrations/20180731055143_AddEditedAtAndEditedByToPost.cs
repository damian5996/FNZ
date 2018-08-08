using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace FNZ.Data.Migrations
{
    public partial class AddEditedAtAndEditedByToPost : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "EditedAt",
                table: "Posts",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "EditedById",
                table: "Posts",
                nullable: true);

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
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Posts_Moderators_EditedById",
                table: "Posts");

            migrationBuilder.DropIndex(
                name: "IX_Posts_EditedById",
                table: "Posts");

            migrationBuilder.DropColumn(
                name: "EditedAt",
                table: "Posts");

            migrationBuilder.DropColumn(
                name: "EditedById",
                table: "Posts");
        }
    }
}
