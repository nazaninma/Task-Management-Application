using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace APProjS.Migrations
{
    /// <inheritdoc />
    public partial class InitialMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "MyTodo",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Taskname = table.Column<string>(type: "TEXT", nullable: false),
                    TaskdeadLine = table.Column<DateTime>(type: "TEXT", nullable: false),
                    TaskperentCompeleted = table.Column<double>(type: "REAL", nullable: false),
                    Tasktimeneeded = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MyTodo", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "MyTodo");
        }
    }
}
