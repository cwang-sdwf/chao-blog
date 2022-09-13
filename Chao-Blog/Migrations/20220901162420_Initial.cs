using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Chao_Blog.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Resumes",
                columns: new[] { "Id", "Educations", "Experiences", "Introduction", "Skills", "UserId" },
                values: new object[] { new Guid("4b501cb3-d168-4cc0-b375-48fb33f333a5"), "testEdutions2", "testexperience2", "testintroduction2", "testskills2", new Guid("bbdee09c-089b-4d30-bece-44df5923716c") });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Resumes",
                keyColumn: "Id",
                keyValue: new Guid("4b501cb3-d168-4cc0-b375-48fb33f333a5"));
        }
    }
}
