using FileSortService.Data;
using Microsoft.EntityFrameworkCore.Migrations;

namespace FileSortService.Migrations
{
    public partial class SQLScript : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            DeserealizeJsonDate deserealizeJsonDate = new DeserealizeJsonDate();
            migrationBuilder.Sql(deserealizeJsonDate.ReturnDateForExtenCategory("Up").ToString());
            migrationBuilder.Sql(deserealizeJsonDate.ReturnDateForExtenValue("Up").ToString());
            migrationBuilder.Sql(deserealizeJsonDate.ReturnDateForUploadCheck("Up").ToString());
            migrationBuilder.Sql(deserealizeJsonDate.ReturnDateForArchitecture("Up").ToString());
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
