using CovAnalytica.Shared.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CovAnalytica.Shared.Interfaces
{
    public interface ICovidDataCSVService
    {
        Task<ICollection<VaccinationLocation>> LocationsFromStringAsync(string csvAsString);
        Task<ICollection<Vaccination>> VaccinationsFromStringAsync(string csvAsString);
        Task<ICollection<CompleteCovidData>> CompleteCovidDataFromStringAsync(string csvAsString);
    }
}
