using CovAnalytica.Shared.Interfaces;
using CovAnalytica.Shared.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CovAnalytica.Shared.Services
{
    public class TimeseriesDataMemoryStorage : IMemoryStorage<CompleteCovidData>
    {
        private List<CompleteCovidData> _timeseriesCovidDataCollection = new List<CompleteCovidData>();
        public Task<List<CompleteCovidData>> GetAll()
        {
            return Task.FromResult(_timeseriesCovidDataCollection);
        }

        public Task<bool> HasDataBeenLoaded()
        {
            return Task.FromResult(_timeseriesCovidDataCollection.Count > 0);
        }

        public Task Save(ICollection<CompleteCovidData> entities)
        {
            if (_timeseriesCovidDataCollection.Any())
            {
                _timeseriesCovidDataCollection.Clear();
            }

            _timeseriesCovidDataCollection.AddRange(entities);

            return Task.CompletedTask;
        }
    }
}
