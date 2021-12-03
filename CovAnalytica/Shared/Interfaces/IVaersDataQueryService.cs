using CovAnalytica.Shared.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CovAnalytica.Shared.Interfaces
{
    public interface IVaersDataQueryService
    {
        Task<List<VaersVaxAdverseEvent>> ListVaxAdverseEventsWithQueryParamsAsync(QueryParams queryParams, bool orderByAscendent);
        Task<List<string>> ListEventCategoriesAsync();
        Task<List<string>> ListVaccineTypesAsync();
        Task<List<string>> ListVaccineManufacturersAsync();
        Task<List<string>> ListVaccineAgeGroupsAsync();
    }
}
