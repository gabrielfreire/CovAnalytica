using CovAnalytica.Shared.Extensions;
using CovAnalytica.Shared.Interfaces;
using CovAnalytica.Shared.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Text;
using System.Threading.Tasks;

namespace CovAnalytica.Shared.Services
{
    public class VaersDataQueryService : IVaersDataQueryService
    {
        private IMemoryStorage<VaersVaxAdverseEvent> _vaersVaxAEMemoryStorage;

        private IApplicationDbContext _dbContext;

		public VaersDataQueryService(
            IMemoryStorage<VaersVaxAdverseEvent> vaersVaxAEMemoryStorage, 
            IApplicationDbContext dbContext)
		{
			_vaersVaxAEMemoryStorage = vaersVaxAEMemoryStorage;
			_dbContext = dbContext;
		}

		public async Task<List<string>> ListEventCategoriesAsync()
        {
            IQueryable<VaersVaxAdverseEvent> _queryable = default;

            if (await _vaersVaxAEMemoryStorage.HasDataBeenLoaded())
            {
                var _list = await _vaersVaxAEMemoryStorage.GetAll();
                _queryable = _list.AsQueryable();
            }
            else
			{
                // fallback to db
                _queryable = _dbContext.VaersVaxAdverseEventItems.AsQueryable();
            }

            return _queryable.GroupBy(l => l.EventCategory).Select(l => l.Key ?? "Unkown").ToList();
        }

        public async Task<List<string>> ListVaccineAgeGroupsAsync()
        {
            IQueryable<VaersVaxAdverseEvent> _queryable = default;

            if (await _vaersVaxAEMemoryStorage.HasDataBeenLoaded())
            {
                var _list = await _vaersVaxAEMemoryStorage.GetAll();
                _queryable = _list.AsQueryable();
            }
            else
            {
                // fallback to db
                _queryable = _dbContext.VaersVaxAdverseEventItems.AsQueryable();
            }

            return _queryable.GroupBy(l => l.AgeCode).Select(l => l.Key ?? "Unkown").ToList();
        }

        public async Task<List<string>> ListVaccineManufacturersAsync()
        {
            IQueryable<VaersVaxAdverseEvent> _queryable = default;

            if (await _vaersVaxAEMemoryStorage.HasDataBeenLoaded())
            {
                var _list = await _vaersVaxAEMemoryStorage.GetAll();
                _queryable = _list.AsQueryable();
            }
            else
            {
                // fallback to db
                _queryable = _dbContext.VaersVaxAdverseEventItems.AsQueryable();
            }

            return _queryable.GroupBy(l => l.VaccineManufacturer).Select(l => l.Key ?? "Unkown").ToList();
        }

        public async Task<List<string>> ListVaccineTypesAsync()
        {
            IQueryable<VaersVaxAdverseEvent> _queryable = default;

            if (await _vaersVaxAEMemoryStorage.HasDataBeenLoaded())
            {
                var _list = await _vaersVaxAEMemoryStorage.GetAll();
                _queryable = _list.AsQueryable();
            }
            else
            {
                // fallback to db
                _queryable = _dbContext.VaersVaxAdverseEventItems.AsQueryable();
            }

            return _queryable.GroupBy(l => l.VaccineType).Select(l => l.Key ?? "Unkown").ToList();
        }

        public Task<List<dynamic>> ListVaxAdverseEventsWithQueryParamsAsync(QueryParams queryParams, bool orderByAscendent)
        {
            return _search(queryParams, orderByAscendent, queryParams.Skip, queryParams.Count);
        }

        private async Task<List<dynamic>> _search(QueryParams queryParams, bool orderByAscendent, int skip = 0, int count = 0)
        {
            IQueryable _queryable = default;

            if (await _vaersVaxAEMemoryStorage.HasDataBeenLoaded())
            {
                var _list = await _vaersVaxAEMemoryStorage.GetAll();
                _queryable = _list.AsQueryable();
            }
            else
			{
                // fallback to db
                _queryable = _dbContext.VaersVaxAdverseEventItems.AsQueryable();
			}

            _queryable = _queryable.BuildQuery<VaersVaxAdverseEvent>(queryParams.GetPropertiesInformation(), queryParams.SelectList, orderByAscendent, skip, count);
            
            return _queryable.ToDynamicList();
        }
    }
}
