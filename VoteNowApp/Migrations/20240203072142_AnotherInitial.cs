using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace VoteNowApp.Migrations
{
    /// <inheritdoc />
    public partial class AnotherInitial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Voters",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    votername = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    number = table.Column<int>(type: "int", nullable: false),
                    course = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    yearlevel = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    password = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Voters", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Voters");
        }
    }
}
