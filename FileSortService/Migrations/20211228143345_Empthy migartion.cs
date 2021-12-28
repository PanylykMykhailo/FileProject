using FileSortService.Data;
using Microsoft.EntityFrameworkCore.Migrations;

namespace FileSortService.Migrations
{
    public partial class Empthymigartion : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            WriteScript writeScript = new WriteScript();
            migrationBuilder.Sql(writeScript.WriteScriptAll<Model.Migrations.ExtenCategory>("DateForExtenCategory.json", "Up").ToString());
            migrationBuilder.Sql(writeScript.WriteScriptAll<Model.Migrations.ExtenValue>("DateForExtenValue.json", "Up").ToString());
            migrationBuilder.Sql(writeScript.WriteScriptAll<Model.Migrations.Architecture>("DateForArchitecture.json", "Up").ToString());
            migrationBuilder.Sql(writeScript.WriteScriptAll<Model.Migrations.UploadCheck>("DateForUploadCheck.json", "Up").ToString());
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            WriteScript writeScript = new WriteScript();
            migrationBuilder.Sql(writeScript.WriteScriptAll<Model.Migrations.UploadCheck>("DateForUploadCheck.json", "Down").ToString());
            migrationBuilder.Sql(writeScript.WriteScriptAll<Model.Migrations.Architecture>("DateForArchitecture.json", "Down").ToString());
            migrationBuilder.Sql(writeScript.WriteScriptAll<Model.Migrations.ExtenValue>("DateForExtenValue.json", "Down").ToString());
            migrationBuilder.Sql(writeScript.WriteScriptAll<Model.Migrations.ExtenCategory>("DateForExtenCategory.json", "Down").ToString());
        }
    }
}
