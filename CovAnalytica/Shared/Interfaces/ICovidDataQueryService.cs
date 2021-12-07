using CovAnalytica.Shared.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CovAnalytica.Shared.Interfaces
{
    public interface ICovidDataQueryService
    {
        Task<List<string>> ListAllContinentsAsync();
        Task<List<Country>> ListAllCountriesAsync();
        Task<List<dynamic>> ListTotalsPerCountryWithQueryParamsAsync(QueryParams queryParams, bool orderByAscendent);
        Task<List<dynamic>> ListTimeseriesDataWithQueryParamsAsync(QueryParams queryParams, bool orderByAscendent);
    }
}
