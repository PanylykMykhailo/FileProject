using FileSortService.Data;
using Microsoft.EntityFrameworkCore.Migrations;

namespace FileSortService.Migrations
{
    public partial class InsertUploadCheck : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            DeserealizeJsonDate deserealizeJsonDate = new DeserealizeJsonDate();
            migrationBuilder.Sql(deserealizeJsonDate.ReturnDateForUploadCheck("Up").ToString());
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            DeserealizeJsonDate deserealizeJsonDate = new DeserealizeJsonDate();
            migrationBuilder.Sql(deserealizeJsonDate.ReturnDateForUploadCheck("Down").ToString());
        }
    }
}
