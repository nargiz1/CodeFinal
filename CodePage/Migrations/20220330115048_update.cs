using Microsoft.EntityFrameworkCore.Migrations;

namespace CodePage.Migrations
{
    public partial class update : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "UodateDate",
                table: "Banners",
                newName: "UpdateDate");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "UpdateDate",
                table: "Banners",
                newName: "UodateDate");
        }
    }
}
