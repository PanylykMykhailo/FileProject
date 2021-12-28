using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace FileSortService.Migrations
{
    public partial class Init : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ExtenCategory",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    nameCategory = table.Column<string>(type: "varchar(20)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ExtenCategory", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Architecture",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    nameFile = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    typeFile = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    typeCategoryId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    linkToOpen = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    sizeFile = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    dateCreatedFile = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    isFolder = table.Column<bool>(type: "bit", nullable: false),
                    fileInFolder = table.Column<int>(type: "int", nullable: false),
                    pathfolder = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Architecture", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Architecture_ExtenCategory_typeCategoryId",
                        column: x => x.typeCategoryId,
                        principalTable: "ExtenCategory",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ExtenValue",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    extensionCategoryId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    extensionValue = table.Column<string>(type: "varchar(20)", nullable: true)
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

            migrationBuilder.CreateTable(
                name: "UploadCheck",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    typeCategoryId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    typeFile = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    hexSignature = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UploadCheck", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UploadCheck_ExtenCategory_typeCategoryId",
                        column: x => x.typeCategoryId,
                        principalTable: "ExtenCategory",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Architecture_typeCategoryId",
                table: "Architecture",
                column: "typeCategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_ExtenValue_extensionCategoryId",
                table: "ExtenValue",
                column: "extensionCategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_UploadCheck_typeCategoryId",
                table: "UploadCheck",
                column: "typeCategoryId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Architecture");

            migrationBuilder.DropTable(
                name: "ExtenValue");

            migrationBuilder.DropTable(
                name: "UploadCheck");

            migrationBuilder.DropTable(
                name: "ExtenCategory");
        }
    }
}
