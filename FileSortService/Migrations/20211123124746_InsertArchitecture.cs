using FileSortService.Data;
using Microsoft.EntityFrameworkCore.Migrations;

namespace FileSortService.Migrations
{
    public partial class InsertArchitecture : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            DeserealizeJsonDate deserealizeJsonDate = new DeserealizeJsonDate();
            migrationBuilder.Sql(deserealizeJsonDate.ReturnDateForArchitecture("Up").ToString());
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            DeserealizeJsonDate deserealizeJsonDate = new DeserealizeJsonDate();
            migrationBuilder.Sql(deserealizeJsonDate.ReturnDateForArchitecture("Down").ToString());
        }
    }
}
