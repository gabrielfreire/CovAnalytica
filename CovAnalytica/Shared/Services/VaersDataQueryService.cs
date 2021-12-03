using CovAnalytica.Shared.Extensions;
using CovAnalytica.Shared.Interfaces;
using CovAnalytica.Shared.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CovAnalytica.Shared.Services
{
    public class VaersDataQueryService : IVaersDataQueryService
    {
        private IMemoryStorage<VaersVaxAdverseEvent> _vaersVaxAEMemoryStorage;

        public VaersDataQueryService(IMemoryStorage<VaersVaxAdverseEvent> vaersVaxAEMemoryStorage)
        {
            _vaersVaxAEMemoryStorage = vaersVaxAEMemoryStorage;
        }

        public async Task<List<string>> ListEventCategoriesAsync()
        {
            if (await _vaersVaxAEMemoryStorage.HasDataBeenLoaded())
            {
                var _list = await _vaersVaxAEMemoryStorage.GetAll();
                return _list.GroupBy(l => l.EventCategory).Select(l => l.Key ?? "Unkown").ToList();
            }
            return new List<string>();
        }

        public async Task<List<string>> ListVaccineAgeGroupsAsync()
        {
            if (await _vaersVaxAEMemoryStorage.HasDataBeenLoaded())
            {
                var _list = await _vaersVaxAEMemoryStorage.GetAll();
                return _list.GroupBy(l => l.AgeCode).Select(l => l.Key ?? "Unkown").ToList();
            }
            return new List<string>();
        }

        public async Task<List<string>> ListVaccineManufacturersAsync()
        {
            if (await _vaersVaxAEMemoryStorage.HasDataBeenLoaded())
            {
                var _list = await _vaersVaxAEMemoryStorage.GetAll();
                return _list.GroupBy(l => l.VaccineManufacturer).Select(l => l.Key ?? "Unkown").ToList();
            }
            return new List<string>();
        }

        public async Task<List<string>> ListVaccineTypesAsync()
        {
            if (await _vaersVaxAEMemoryStorage.HasDataBeenLoaded())
            {
                var _list = await _vaersVaxAEMemoryStorage.GetAll();
                return _list.GroupBy(l => l.VaccineType).Select(l => l.Key ?? "Unkown").ToList();
            }
            return new List<string>();
        }

        public Task<List<VaersVaxAdverseEvent>> ListVaxAdverseEventsWithQueryParamsAsync(QueryParams queryParams, bool orderByAscendent)
        {
            return _search(queryParams, orderByAscendent, queryParams.Skip, queryParams.Count);
        }

        private async Task<List<VaersVaxAdverseEvent>> _search(QueryParams queryParams, bool orderByAscendent, int skip = 0, int count = 0)
        {
            if (await _vaersVaxAEMemoryStorage.HasDataBeenLoaded())
            {
                var _list = await _vaersVaxAEMemoryStorage.GetAll();
                var _queryable = _list.BuildQuery(queryParams.GetPropertiesInformation(), queryParams.SelectList, orderByAscendent, skip, count);
                return _queryable.ToList();
            }
            return new List<VaersVaxAdverseEvent>();
        }
    }
}
