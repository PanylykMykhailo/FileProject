using FileSortService.Data;
using Microsoft.EntityFrameworkCore.Migrations;

namespace FileSortService.Migrations
{
    public partial class SQLScript : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            DeserealizeJsonDate deserealizeJsonDate = new DeserealizeJsonDate();
            //doSCSRIPT doSCSRIPT = new doSCSRIPT();
            migrationBuilder.Sql(deserealizeJsonDate.ReturnDateForExtenCategory("Up").ToString());
            migrationBuilder.Sql(deserealizeJsonDate.ReturnDateForExtenValue("Up").ToString());
            migrationBuilder.Sql(deserealizeJsonDate.ReturnDateForArchitecture("Up").ToString());
            migrationBuilder.Sql(deserealizeJsonDate.ReturnDateForUploadCheck("Up").ToString());
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            DeserealizeJsonDate deserealizeJsonDate = new DeserealizeJsonDate();
            migrationBuilder.Sql(deserealizeJsonDate.ReturnDateForExtenCategory("Down").ToString());
            migrationBuilder.Sql(deserealizeJsonDate.ReturnDateForExtenValue("Down").ToString());
            migrationBuilder.Sql(deserealizeJsonDate.ReturnDateForArchitecture("Down").ToString());
            migrationBuilder.Sql(deserealizeJsonDate.ReturnDateForUploadCheck("Down").ToString());
        }
    }
}
