using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CovAnalytica.Shared.Models
{
    //location,iso_code,date,total_vaccinations,people_vaccinated,
    //people_fully_vaccinated,total_boosters,daily_vaccinations_raw,
    //daily_vaccinations,total_vaccinations_per_hundred,people_vaccinated_per_hundred,
    //people_fully_vaccinated_per_hundred,total_boosters_per_hundred,daily_vaccinations_per_million,
    //daily_people_vaccinated,daily_people_vaccinated_per_hundred
    public class Vaccination
    {
        public string? Location { get; set; }
        public string? IsoCode { get; set; }
        public DateTime Date { get; set; }
        public decimal? TotalVaccinations { get; set; }
        public decimal? PeopleVaccinated { get; set; }
        public decimal? PeopleFullyVaccinated { get; set; }
        public decimal? TotalBoosters { get; set; }
        public decimal? DailyVaccinationsRaw { get; set; }
        public decimal? DailyVaccinations { get; set; }
        public decimal? TotalVaccinationsPerHundred { get; set; }
        public decimal? PeopleVaccinatedPerHundrer { get; set; }
        public decimal? PeopleFullyVaccinatedPerHundrer { get; set; }
        public decimal? TotalBoostersPerHundred { get; set; }
        public decimal? DailyVaccinationsPerMillion { get; set; }
        public decimal? DailyPeopleVaccinated { get; set; }
        public decimal? DailyPeopleVaccinatedPerHundred { get; set; }
    }
}
