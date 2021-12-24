using FileSortService.Data;
using Microsoft.EntityFrameworkCore.Migrations;

namespace FileSortService.Migrations
{
    public partial class Empthymigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            WriteScript writeScript = new WriteScript();
            migrationBuilder.Sql(writeScript.WriteScriptAll<Model.Migrations.ExtensionCategory>("DateForExtenCategory.json","Up").ToString());
            migrationBuilder.Sql(writeScript.WriteScriptAll<Model.Migrations.ExtensionValue>("DateForExtenValue.json", "Up").ToString());
            migrationBuilder.Sql(writeScript.WriteScriptAll<Model.Migrations.Architecture>("DateForArchitecture.json", "Up").ToString());
            migrationBuilder.Sql(writeScript.WriteScriptAll<Model.Migrations.UploadCheck>("DateForUploadCheck.json", "Up").ToString());
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            WriteScript writeScript = new WriteScript();
            migrationBuilder.Sql(writeScript.WriteScriptAll<Model.Migrations.UploadCheck>("DateForUploadCheck.json", "Down").ToString());
            migrationBuilder.Sql(writeScript.WriteScriptAll<Model.Migrations.Architecture>("DateForArchitecture.json", "Down").ToString());
            migrationBuilder.Sql(writeScript.WriteScriptAll<Model.Migrations.ExtensionValue>("DateForExtenValue.json", "Down").ToString());
            migrationBuilder.Sql(writeScript.WriteScriptAll<Model.Migrations.ExtensionCategory>("DateForExtenCategory.json", "Down").ToString());
        }
    }
}
