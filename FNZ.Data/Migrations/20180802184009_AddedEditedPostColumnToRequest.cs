using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace FNZ.Data.Migrations
{
    public partial class AddedEditedPostColumnToRequest : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "EditedPostId",
                table: "Requests",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Requests_EditedPostId",
                table: "Requests",
                column: "EditedPostId");

            migrationBuilder.AddForeignKey(
                name: "FK_Requests_Posts_EditedPostId",
                table: "Requests",
                column: "EditedPostId",
                principalTable: "Posts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Requests_Posts_EditedPostId",
                table: "Requests");

            migrationBuilder.DropIndex(
                name: "IX_Requests_EditedPostId",
                table: "Requests");

            migrationBuilder.DropColumn(
                name: "EditedPostId",
                table: "Requests");
        }
    }
}
