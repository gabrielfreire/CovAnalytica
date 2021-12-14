using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CovAnalytica.Server.Data.Migrations
{
    public partial class addnewvaccinationdataattributes : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<decimal>(
                name: "NewPeopleVaccinatedSmoothed",
                table: "CompleteCovidDataItems",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "NewPeopleVaccinatedSmoothedPerHundred",
                table: "CompleteCovidDataItems",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "NewVaccinationsSmoothedPerMillion",
                table: "CompleteCovidDataItems",
                type: "TEXT",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "NewPeopleVaccinatedSmoothed",
                table: "CompleteCovidDataItems");

            migrationBuilder.DropColumn(
                name: "NewPeopleVaccinatedSmoothedPerHundred",
                table: "CompleteCovidDataItems");

            migrationBuilder.DropColumn(
                name: "NewVaccinationsSmoothedPerMillion",
                table: "CompleteCovidDataItems");
        }
    }
}
