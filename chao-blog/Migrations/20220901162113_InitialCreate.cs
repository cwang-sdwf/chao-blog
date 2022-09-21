using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Chao_Blog.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    FirstName = table.Column<string>(nullable: true),
                    LastName = table.Column<string>(nullable: true),
                    Password = table.Column<string>(nullable: true),
                    UserLevel = table.Column<int>(maxLength: 1, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Resumes",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    UserId = table.Column<Guid>(nullable: false),
                    Introduction = table.Column<string>(maxLength: 1000, nullable: false),
                    Skills = table.Column<string>(maxLength: 1000, nullable: false),
                    Experiences = table.Column<string>(maxLength: 5000, nullable: false),
                    Educations = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Resumes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Resumes_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "FirstName", "LastName", "Password", "UserLevel" },
                values: new object[] { new Guid("bbdee09c-089b-4d30-bece-44df5923716c"), "Chao", "Wang", "wc1234", 2 });

            migrationBuilder.InsertData(
                table: "Resumes",
                columns: new[] { "Id", "Educations", "Experiences", "Introduction", "Skills", "UserId" },
                values: new object[] { new Guid("4b501cb3-d168-4cc0-b375-48fb33f318a4"), "testEdutions1", "testexperience1", "testintroduction1", "testskills1", new Guid("bbdee09c-089b-4d30-bece-44df5923716c") });

            migrationBuilder.CreateIndex(
                name: "IX_Resumes_UserId",
                table: "Resumes",
                column: "UserId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Resumes");

            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}
