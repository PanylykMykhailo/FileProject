using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace FileSortService.Migrations
{
    public partial class InitialDB : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ExtenCategory",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    nameCategory = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ExtenCategory", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ExtenValue",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    extensionCategoryId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    extensionValue = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ExtenValue", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ExtenValue_ExtenCategory_extensionCategoryId",
                        column: x => x.extensionCategoryId,
                        principalTable: "ExtenCategory",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ExtenValue_extensionCategoryId",
                table: "ExtenValue",
                column: "extensionCategoryId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ExtenValue");

            migrationBuilder.DropTable(
                name: "ExtenCategory");
        }
    }
}
