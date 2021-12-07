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

        private IApplicationDbContext _dbContext;

		public CovidDataQueryService(
			IMemoryStorage<CompleteCovidData> completeCovidDataMemoryStorage,
			IMemoryStorage<SelectionCovidData> selectionCovidDataMemoryStorage, 
            IApplicationDbContext dbContext)
		{
			_completeCovidDataMemoryStorage = completeCovidDataMemoryStorage;
			_selectionCovidDataMemoryStorage = selectionCovidDataMemoryStorage;
			_dbContext = dbContext;
		}

		public async Task<List<string>> ListAllContinentsAsync()
        {
            IQueryable<CompleteCovidData> _queryable = default;

            if (await _completeCovidDataMemoryStorage.HasDataBeenLoaded())
            {
                var _list = await _completeCovidDataMemoryStorage.GetAll();
                _queryable = _list.AsQueryable();
            }
            else
            {
                // fallback to db
                _queryable = _dbContext.CompleteCovidDataItems.AsQueryable();
            }

            return _queryable.GroupBy(q => q.Continent).Select(q => q.Key).ToList();
        }

        public async Task<List<Country>> ListAllCountriesAsync()
        {
            IQueryable<CompleteCovidData> _queryable = default;

            if (await _completeCovidDataMemoryStorage.HasDataBeenLoaded())
            {
                var _list = await _completeCovidDataMemoryStorage.GetAll();
                _queryable = _list.AsQueryable();
            }
            else
            {
                // fallback to db
                _queryable = _dbContext.CompleteCovidDataItems.AsQueryable();
            }

            return _queryable
                .GroupBy(q => q.Location)
                .Select(q => new Country() { Name = q.Key, Continent = q.First().Continent, IsoCode = q.First().IsoCode })
                .ToList();
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
            IQueryable _queryable = default;
            if (await _completeCovidDataMemoryStorage.HasDataBeenLoaded())
            {
                var _list = await _completeCovidDataMemoryStorage.GetAll();
                _queryable = _list.AsQueryable();
            }
            else
			{
                // fallback to db
                _queryable = _dbContext.CompleteCovidDataItems.AsQueryable();
            }

            _queryable = _queryable.BuildQuery<CompleteCovidData>(queryParams.GetPropertiesInformation(), queryParams.SelectList, orderByAscendent, skip, count);

            return _queryable.ToDynamicList();
            
        }

        private async Task<List<dynamic>> _searchSelection(QueryParams queryParams, bool orderByAscendent, int skip=0, int count=0)
        {
            IQueryable _queryable = default;
            if (await _selectionCovidDataMemoryStorage.HasDataBeenLoaded())
            {
                var _list = await _selectionCovidDataMemoryStorage.GetAll();
                _queryable = _list.AsQueryable();
            }
            else
            {
                // fallback to db
                _queryable = _dbContext.SelectionCovidDataItems.AsQueryable();
            }

            _queryable = _queryable.BuildQuery<SelectionCovidData>(queryParams.GetPropertiesInformation(), queryParams.SelectList, orderByAscendent, skip, count);

            return _queryable.ToDynamicList();
        }

        
    }
}
