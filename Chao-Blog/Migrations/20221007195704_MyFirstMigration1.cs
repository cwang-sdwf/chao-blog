using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Chao_Blog.Migrations
{
    public partial class MyFirstMigration1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Resumes_Users_UserId",
                table: "Resumes");

            migrationBuilder.DeleteData(
                table: "Resumes",
                keyColumn: "Id",
                keyValue: new Guid("4b501cb3-d168-4cc0-b375-48fb33f318a4"));

            migrationBuilder.DeleteData(
                table: "Resumes",
                keyColumn: "Id",
                keyValue: new Guid("4b501cb3-d168-4cc0-b375-48fb33f333a5"));

            migrationBuilder.AddForeignKey(
                name: "FK_Resumes_Users_UserId",
                table: "Resumes",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Resumes_Users_UserId",
                table: "Resumes");

            migrationBuilder.InsertData(
                table: "Resumes",
                columns: new[] { "Id", "Educations", "Experiences", "Introduction", "Skills", "UserId" },
                values: new object[] { new Guid("4b501cb3-d168-4cc0-b375-48fb33f318a4"), "testEdutions1", "testexperience1", "testintroduction1", "testskills1", new Guid("bbdee09c-089b-4d30-bece-44df5923716c") });

            migrationBuilder.InsertData(
                table: "Resumes",
                columns: new[] { "Id", "Educations", "Experiences", "Introduction", "Skills", "UserId" },
                values: new object[] { new Guid("4b501cb3-d168-4cc0-b375-48fb33f333a5"), "testEdutions2", "testexperience2", "testintroduction2", "testskills2", new Guid("bbdee09c-089b-4d30-bece-44df5923716c") });

            migrationBuilder.AddForeignKey(
                name: "FK_Resumes_Users_UserId",
                table: "Resumes",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
