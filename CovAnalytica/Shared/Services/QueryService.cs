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
    public class QueryService : IQueryService
    {
        private IMemoryStorage<CompleteCovidData> _completeCovidDataMemoryStorage;
        private IMemoryStorage<SelectionCovidData> _selectionCovidDataMemoryStorage;

        public QueryService(
            IMemoryStorage<CompleteCovidData> completeCovidDataMemoryStorage,
            IMemoryStorage<SelectionCovidData> selectionCovidDataMemoryStorage)
        {
            _completeCovidDataMemoryStorage = completeCovidDataMemoryStorage;
            _selectionCovidDataMemoryStorage = selectionCovidDataMemoryStorage;
        }

        public Task<List<CompleteCovidData>> ListTimeseriesDataWithQueryParamsAsync(QueryParams queryParams, bool orderByAscendent)
        {
            return _searchComplete(queryParams.GetPropertiesInformation(), orderByAscendent, queryParams.Skip, queryParams.Count);
        }

        public Task<List<SelectionCovidData>> ListTotalsPerCountryWithQueryParamsAsync(QueryParams queryParams, bool orderByAscendent)
        {
            return _searchSelection(queryParams.GetPropertiesInformation(), orderByAscendent, queryParams.Skip, queryParams.Count);
        }

        public Task<List<CompleteCovidData>> SearchByContinentAsync(string continentName, bool orderByAscendent, int skip, int count)
        {
            var _propInfo = new PropertyInformation(nameof(CompleteCovidData.Continent), continentName);
            
            return _searchComplete(new List<PropertyInformation>() { _propInfo }, orderByAscendent, skip, count);
        }

        public Task<List<CompleteCovidData>> SearchByCountryAsync(string countryName, bool orderByAscendent, int skip, int count)
        {
            var _propInfo = new PropertyInformation(nameof(CompleteCovidData.Location), countryName);

            return _searchComplete(new List<PropertyInformation>() { _propInfo }, orderByAscendent, skip, count);
        }

        private async Task<List<CompleteCovidData>> _searchComplete(List<PropertyInformation> propertyInformations, bool orderByAscendent, int skip=0, int count=0)
        {
            if (await _completeCovidDataMemoryStorage.HasDataBeenLoaded())
            {
                var _list = await _completeCovidDataMemoryStorage.GetAll();
                var _queryable = GetQueryable(_list, propertyInformations, orderByAscendent, skip, count);
                return _queryable.ToList();
            }
            return new List<CompleteCovidData>();
        }
        private async Task<List<SelectionCovidData>> _searchSelection(List<PropertyInformation> propertyInformations, bool orderByAscendent, int skip=0, int count=0)
        {
            if (await _selectionCovidDataMemoryStorage.HasDataBeenLoaded())
            {
                var _list = await _selectionCovidDataMemoryStorage.GetAll();
                var _queryable = GetQueryable(_list, propertyInformations, orderByAscendent, skip, count);
                return _queryable.ToList();
            }
            return new List<SelectionCovidData>();
        }

        private IQueryable<T> GetQueryable<T>(List<T> list, List<PropertyInformation> propertyInformations, bool orderByAscendent, int skip = 0, int count = 0)
        {
            var _queryable = list.AsQueryable();
            if (_queryable.Any())
            {

                var _criteriasSb = new StringBuilder();

                foreach (var property in propertyInformations)
                {
                    if (property.Value == null)
                        continue;

                    if (_criteriasSb.Length > 0)
                    {
                        _criteriasSb.Append(@$" AND it.{property.Name} = ""{property.Value}""");
                    }
                    else
                    {
                        _criteriasSb.Append(@$"it.{property.Name} = ""{property.Value}""");
                    }
                }

                if (_criteriasSb.Length > 0)
                {
                    _queryable = _queryable.Where(_criteriasSb.ToString());
                }

                if (orderByAscendent)
                {
                    _queryable = _queryable.OrderBy("it.Date");
                }
                else
                {
                    _queryable = _queryable.OrderByDescending(it => "it.Date");
                }

                _queryable = _queryable.Skip(skip);

                if (count > 0)
                    _queryable = _queryable.Take(count);

            }
            return _queryable;
        }
    }
}
