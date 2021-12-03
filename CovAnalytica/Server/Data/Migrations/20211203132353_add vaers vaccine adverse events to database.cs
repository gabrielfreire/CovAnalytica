using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CovAnalytica.Server.Data.Migrations
{
    public partial class addvaersvaccineadverseeventstodatabase : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "VaersVaxAdverseEventItems",
                columns: table => new
                {
                    Id = table.Column<string>(type: "TEXT", nullable: false),
                    VaccineType = table.Column<string>(type: "TEXT", nullable: true),
                    VaccineTypeCode = table.Column<string>(type: "TEXT", nullable: true),
                    VaccineDose = table.Column<string>(type: "TEXT", nullable: true),
                    VaccineDoseCode = table.Column<string>(type: "TEXT", nullable: true),
                    VaccineManufacturer = table.Column<string>(type: "TEXT", nullable: true),
                    VaccineManufacturerCode = table.Column<string>(type: "TEXT", nullable: true),
                    EventCategory = table.Column<string>(type: "TEXT", nullable: true),
                    EventCategoryCode = table.Column<string>(type: "TEXT", nullable: true),
                    Age = table.Column<string>(type: "TEXT", nullable: true),
                    AgeCode = table.Column<string>(type: "TEXT", nullable: true),
                    EventsReported = table.Column<decimal>(type: "TEXT", nullable: true),
                    Percent = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VaersVaxAdverseEventItems", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "VaersVaxAdverseEventItems");
        }
    }
}
