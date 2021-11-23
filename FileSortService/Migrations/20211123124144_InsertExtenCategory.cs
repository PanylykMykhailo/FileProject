using FileSortService.Data;
using Microsoft.EntityFrameworkCore.Migrations;

namespace FileSortService.Migrations
{
    public partial class InsertExtenCategory : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            DeserealizeJsonDate deserealizeJsonDate = new DeserealizeJsonDate();
            migrationBuilder.Sql(deserealizeJsonDate.ReturnDateForExtenCategory("Up").ToString());
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            DeserealizeJsonDate deserealizeJsonDate = new DeserealizeJsonDate();
            migrationBuilder.Sql(deserealizeJsonDate.ReturnDateForExtenCategory("Down").ToString());
        }
    }
}
