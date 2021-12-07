using CovAnalytica.Shared.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CovAnalytica.Shared.Services
{
    public class SDKService
    {
        private HttpClient _httpClient;

        public SDKService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public Task<List<CompleteCovidData>?> ListTimeseriesByCountry(string countryName, string selects = "")
        {
            return GetAsync<List<CompleteCovidData>>($"/data/timeseries-covid-data?Location={countryName}{(!string.IsNullOrWhiteSpace(selects) ? $"&selects={selects}" : "")}");
        }
        public Task<List<Country>?> ListCountries()
        {
            return GetAsync<List<Country>>($"/data/countries");
        }
        public Task<List<string>?> ListContinents()
        {
            return GetAsync<List<string>>($"/data/continents");
        }

        private async Task<T?> GetAsync<T>(string url)
        {
            try
            {
                var result = await _httpClient.GetAsync(url);
                var content = await result.Content.ReadAsStringAsync();
                if (result.IsSuccessStatusCode)
                {
                    return string.IsNullOrWhiteSpace(content) ?
                        default :
                        JsonConvert.DeserializeObject<T>(content);
                }

                throw new Exception(content);
            }
            catch
            {
                throw;
            }
        }
    }
}
