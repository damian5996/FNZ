using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace FNZ.Data.Migrations
{
    public partial class FinishedModels : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "RequestSentAt",
                table: "Posts");

            migrationBuilder.AddColumn<int>(
                name: "ModeratorId",
                table: "Requests",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "SentAt",
                table: "Requests",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<long>(
                name: "PostId",
                table: "MoneyCollections",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Requests_ModeratorId",
                table: "Requests",
                column: "ModeratorId");

            migrationBuilder.CreateIndex(
                name: "IX_MoneyCollections_PostId",
                table: "MoneyCollections",
                column: "PostId");

            migrationBuilder.AddForeignKey(
                name: "FK_MoneyCollections_Posts_PostId",
                table: "MoneyCollections",
                column: "PostId",
                principalTable: "Posts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Requests_Moderators_ModeratorId",
                table: "Requests",
                column: "ModeratorId",
                principalTable: "Moderators",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MoneyCollections_Posts_PostId",
                table: "MoneyCollections");

            migrationBuilder.DropForeignKey(
                name: "FK_Requests_Moderators_ModeratorId",
                table: "Requests");

            migrationBuilder.DropIndex(
                name: "IX_Requests_ModeratorId",
                table: "Requests");

            migrationBuilder.DropIndex(
                name: "IX_MoneyCollections_PostId",
                table: "MoneyCollections");

            migrationBuilder.DropColumn(
                name: "ModeratorId",
                table: "Requests");

            migrationBuilder.DropColumn(
                name: "SentAt",
                table: "Requests");

            migrationBuilder.DropColumn(
                name: "PostId",
                table: "MoneyCollections");

            migrationBuilder.AddColumn<DateTime>(
                name: "RequestSentAt",
                table: "Posts",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }
    }
}
