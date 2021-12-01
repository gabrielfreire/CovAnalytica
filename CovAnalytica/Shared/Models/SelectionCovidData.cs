using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CovAnalytica.Shared.Models
{
    public class SelectionCovidData
    {
        public string Id { get; set; } = Guid.NewGuid().ToString();
        public string Continent { get; set; } = string.Empty;
        public string Location { get; set; } = string.Empty;
        public DateTime Date { get; set; }
        public decimal? TotalCases { get; set; }
        public decimal? TotalDeaths { get; set; }
        public decimal? TotalCasesPerMillion { get; set; }
        public decimal? TotalDeathsPerMillion { get; set; }
        public decimal? IcuPatients { get; set; }
        public decimal? IcuPatientsPerMillion { get; set; }
        public decimal? PeopleVaccinated { get; set; }
        public decimal? TotalBoosters { get; set; }
        public decimal? TotalBoostersPerHundred { get; set; }
        public decimal? PeopleVaccinatedPerHundred { get; set; }
        public decimal? PeopleFullyVaccinated { get; set; }
        public decimal? PeopleFullyVaccinatedPerHundred { get; set; }
        public decimal? Population { get; set; }
        public decimal? MedianAge { get; set; }
        public decimal? Aged65Older { get; set; }
        public decimal? Aged70Older { get; set; }
        public decimal? CardiovascularDeathRate { get; set; }
        public decimal? DiabetesPrevalence { get; set; }
        public decimal? FemaleSmokers { get; set; }
        public decimal? MaleSmokers { get; set; }

    }
}
