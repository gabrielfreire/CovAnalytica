using CovAnalytica.Shared.Interfaces;
using CovAnalytica.Shared.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CovAnalytica.Shared.Services
{
    public class VaersVaxAdverseEventsMemoryStorage : IMemoryStorage<VaersVaxAdverseEvent>
    {
        private List<VaersVaxAdverseEvent> _vaersVaxAdverseEventDataCollection = new List<VaersVaxAdverseEvent>();
        public Task<List<VaersVaxAdverseEvent>> GetAll()
        {
            return Task.FromResult(_vaersVaxAdverseEventDataCollection);
        }

        public Task<bool> HasDataBeenLoaded()
        {
            return Task.FromResult(_vaersVaxAdverseEventDataCollection.Count > 0);
        }

        public Task Save(ICollection<VaersVaxAdverseEvent> entities)
        {
            if (_vaersVaxAdverseEventDataCollection.Any())
            {
                _vaersVaxAdverseEventDataCollection.Clear();
            }

            _vaersVaxAdverseEventDataCollection.AddRange(entities);

            return Task.CompletedTask;
        }
    }
}
