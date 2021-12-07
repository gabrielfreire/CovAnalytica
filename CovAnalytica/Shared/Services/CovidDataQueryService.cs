using CovAnalytica.Shared.Extensions;
using CovAnalytica.Shared.Interfaces;
using CovAnalytica.Shared.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic;
using System.Linq.Dynamic.Core;
using System.Text;
using System.Threading.Tasks;

namespace CovAnalytica.Shared.Services
{
    public class CovidDataQueryService : ICovidDataQueryService
    {
        private IMemoryStorage<CompleteCovidData> _completeCovidDataMemoryStorage;
        private IMemoryStorage<SelectionCovidData> _selectionCovidDataMemoryStorage;

        public CovidDataQueryService(
            IMemoryStorage<CompleteCovidData> completeCovidDataMemoryStorage,
            IMemoryStorage<SelectionCovidData> selectionCovidDataMemoryStorage)
        {
            _completeCovidDataMemoryStorage = completeCovidDataMemoryStorage;
            _selectionCovidDataMemoryStorage = selectionCovidDataMemoryStorage;
        }

        public async Task<List<string>> ListAllContinentsAsync()
        {
            if (await _completeCovidDataMemoryStorage.HasDataBeenLoaded())
            {
                var _list = await _completeCovidDataMemoryStorage.GetAll();
                var _queryable = _list.AsQueryable();
                return _queryable.GroupBy(q => q.Continent).Select(q => q.Key).ToList();
            }
            return new List<string>();
        }

        public async Task<List<Country>> ListAllCountriesAsync()
        {
            if (await _completeCovidDataMemoryStorage.HasDataBeenLoaded())
            {
                var _list = await _completeCovidDataMemoryStorage.GetAll();
                var _queryable = _list.AsQueryable();
                return _queryable.GroupBy(q => q.Location).Select(q => new Country() { Name = q.Key, Continent = q.First().Continent, IsoCode = q.First().IsoCode }).ToList();
            }
            return new List<Country>();
        }

        public Task<List<dynamic>> ListTimeseriesDataWithQueryParamsAsync(QueryParams queryParams, bool orderByAscendent)
        {
            return _searchComplete(queryParams, orderByAscendent, queryParams.Skip, queryParams.Count);
        }

        public Task<List<dynamic>> ListTotalsPerCountryWithQueryParamsAsync(QueryParams queryParams, bool orderByAscendent)
        {
            return _searchSelection(queryParams, orderByAscendent, queryParams.Skip, queryParams.Count);
        }

        private async Task<List<dynamic>> _searchComplete(QueryParams queryParams, bool orderByAscendent, int skip=0, int count=0)
        {
            if (await _completeCovidDataMemoryStorage.HasDataBeenLoaded())
            {
                var _list = await _completeCovidDataMemoryStorage.GetAll();
                var _queryable = _list.BuildQuery(queryParams.GetPropertiesInformation(), queryParams.SelectList, orderByAscendent, skip, count);
                return _queryable.ToDynamicList();
            }
            return new List<dynamic>();
        }

        private async Task<List<dynamic>> _searchSelection(QueryParams queryParams, bool orderByAscendent, int skip=0, int count=0)
        {
            if (await _selectionCovidDataMemoryStorage.HasDataBeenLoaded())
            {
                var _list = await _selectionCovidDataMemoryStorage.GetAll();
                var _queryable = _list.BuildQuery(queryParams.GetPropertiesInformation(), queryParams.SelectList, orderByAscendent, skip, count);
                return _queryable.ToDynamicList();
            }
            return new List<dynamic>();
        }

        
    }
}
