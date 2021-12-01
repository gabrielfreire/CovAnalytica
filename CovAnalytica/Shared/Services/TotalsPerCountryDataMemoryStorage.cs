using CovAnalytica.Shared.Interfaces;
using CovAnalytica.Shared.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CovAnalytica.Shared.Services
{
    public class TotalsPerCountryDataMemoryStorage : IMemoryStorage<SelectionCovidData>
    {
        private List<SelectionCovidData> _totalsPerCountryCovidDataCollection = new List<SelectionCovidData>();
        public Task<List<SelectionCovidData>> GetAll()
        {
            return Task.FromResult(_totalsPerCountryCovidDataCollection);
        }

        public Task<bool> HasDataBeenLoaded()
        {
            return Task.FromResult(_totalsPerCountryCovidDataCollection.Count > 0);
        }

        public Task Save(ICollection<SelectionCovidData> entities)
        {
            if (_totalsPerCountryCovidDataCollection.Any())
            {
                _totalsPerCountryCovidDataCollection.Clear();
            }

            _totalsPerCountryCovidDataCollection.AddRange(entities);

            return Task.CompletedTask;
        }
    }
}
