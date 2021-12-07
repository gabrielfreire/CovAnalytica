using CovAnalytica.Shared.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Text;
using System.Threading.Tasks;

namespace CovAnalytica.Shared.Extensions
{
    public static class ListExtensions
    {
        public static List<SelectionCovidData> ToSelectionCovidData(this List<CompleteCovidData> completeCovidData)
        {
            var _queryable = from data in completeCovidData.AsQueryable()
                             where data.PeopleFullyVaccinatedPerHundred != null
                             group data by data.Location into locationGroup
                             select new SelectionCovidData()
                             {
                                 TotalCases = locationGroup.Select(x => x.TotalCases).Max(),
                                 TotalBoosters = locationGroup.Select(x => x.TotalBoosters).Max(),
                                 TotalBoostersPerHundred = locationGroup.Select(x => x.TotalBoostersPerHundred).Max(),
                                 TotalCasesPerMillion = locationGroup.Select(x => x.TotalCasesPerMillion).Max(),
                                 TotalDeaths = locationGroup.Select(x => x.TotalDeaths).Max(),
                                 TotalDeathsPerMillion = locationGroup.Select(x => x.TotalDeathsPerMillion).Max(),
                                 Aged65Older = locationGroup.Select(x => x.Aged65Older).Max(),
                                 Aged70Older = locationGroup.Select(x => x.Aged70Older).Max(),
                                 CardiovascularDeathRate = locationGroup.Select(x => x.CardiovascularDeathRate).Max(),
                                 DiabetesPrevalence = locationGroup.Select(x => x.DiabetesPrevalence).Max(),
                                 FemaleSmokers = locationGroup.Select(x => x.FemaleSmokers).Max(),
                                 MaleSmokers = locationGroup.Select(x => x.MaleSmokers).Max(),
                                 IcuPatients = locationGroup.Select(x => x.IcuPatients).Max(),
                                 IcuPatientsPerMillion = locationGroup.Select(x => x.IcuPatientsPerMillion).Max(),
                                 MedianAge = locationGroup.Select(x => x.MedianAge).Max(),
                                 PeopleFullyVaccinated = locationGroup.Select(x => x.PeopleFullyVaccinated).Max(),
                                 PeopleFullyVaccinatedPerHundred = locationGroup.Select(x => x.PeopleFullyVaccinatedPerHundred).Max(),
                                 PeopleVaccinated = locationGroup.Select(x => x.PeopleVaccinated).Max(),
                                 PeopleVaccinatedPerHundred = locationGroup.Select(x => x.PeopleVaccinatedPerHundred).Max(),
                                 Population = locationGroup.Select(x => x.Population).Max(),
                                 Continent = locationGroup.First().Continent,
                                 Location = locationGroup.First().Location,
                                 Date = locationGroup.First().Date
                             };
            return _queryable.ToList();
        }

        public static List<SelectionCovidData> ToSelectionCovidData(this ICollection<CompleteCovidData> completeCovidData)
        {
            var _queryable = from data in completeCovidData.AsQueryable()
                             where data.PeopleFullyVaccinatedPerHundred != null
                             group data by data.Location into locationGroup
                             select new SelectionCovidData()
                             {
                                 TotalCases = locationGroup.Select(x => x.TotalCases).Max(),
                                 TotalBoosters = locationGroup.Select(x => x.TotalBoosters).Max(),
                                 TotalBoostersPerHundred = locationGroup.Select(x => x.TotalBoostersPerHundred).Max(),
                                 TotalCasesPerMillion = locationGroup.Select(x => x.TotalCasesPerMillion).Max(),
                                 TotalDeaths = locationGroup.Select(x => x.TotalDeaths).Max(),
                                 TotalDeathsPerMillion = locationGroup.Select(x => x.TotalDeathsPerMillion).Max(),
                                 Aged65Older = locationGroup.Select(x => x.Aged65Older).Max(),
                                 Aged70Older = locationGroup.Select(x => x.Aged70Older).Max(),
                                 CardiovascularDeathRate = locationGroup.Select(x => x.CardiovascularDeathRate).Max(),
                                 DiabetesPrevalence = locationGroup.Select(x => x.DiabetesPrevalence).Max(),
                                 FemaleSmokers = locationGroup.Select(x => x.FemaleSmokers).Max(),
                                 MaleSmokers = locationGroup.Select(x => x.MaleSmokers).Max(),
                                 IcuPatients = locationGroup.Select(x => x.IcuPatients).Max(),
                                 IcuPatientsPerMillion = locationGroup.Select(x => x.IcuPatientsPerMillion).Max(),
                                 MedianAge = locationGroup.Select(x => x.MedianAge).Max(),
                                 PeopleFullyVaccinated = locationGroup.Select(x => x.PeopleFullyVaccinated).Max(),
                                 PeopleFullyVaccinatedPerHundred = locationGroup.Select(x => x.PeopleFullyVaccinatedPerHundred).Max(),
                                 PeopleVaccinated = locationGroup.Select(x => x.PeopleVaccinated).Max(),
                                 PeopleVaccinatedPerHundred = locationGroup.Select(x => x.PeopleVaccinatedPerHundred).Max(),
                                 Population = locationGroup.Select(x => x.Population).Max(),
                                 Continent = locationGroup.First().Continent,
                                 Location = locationGroup.First().Location,
                                 Date = locationGroup.First().Date
                             };
            return _queryable.ToList();
        }

        public static IQueryable BuildQuery<T>(
            this List<T> list, 
            List<PropertyInformation> propertyInformations,
            List<string> selects,
            bool orderByAscendent,
            int skip = 0, int count = 0)
        {
            var _queryable = (IQueryable) list.AsQueryable();
            return _queryable.BuildQuery<T>(propertyInformations, selects, orderByAscendent, skip, count);
        }

        public static IQueryable BuildQuery<T>(
            this IQueryable queryable,
            List<PropertyInformation> propertyInformations,
            List<string> selects,
            bool orderByAscendent,
            int skip = 0, int count = 0)
        {
            if (queryable.Any())
            {

                var _criteriasSb = new StringBuilder();
                var _selectSb = new StringBuilder();

                foreach (var property in propertyInformations)
                {
                    if (property.Value == null || property.Name.ToLower() == "selects")
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

                if (selects.Count > 0)
                {
                    _selectSb.Append("new(");
                    _selectSb.Append(string.Join(",", selects.Select(s => $"it.{s}")));
                    _selectSb.Append(")");
                }

                if (_criteriasSb.Length > 0)
                {
                    queryable = queryable.Where(_criteriasSb.ToString());
                }

                if (typeof(T).GetProperty("Date") != null)
                {
                    if (orderByAscendent)
                    {
                        queryable = queryable.OrderBy("it.Date ASC");
                    }
                    else
                    {
                        queryable = queryable.OrderBy("it.Date DESC");
                    }
                }

                queryable = queryable.Skip(skip);

                if (count > 0)
                    queryable = queryable.Take(count);

                if (_selectSb.Length > 0)
                    queryable = queryable.Select(_selectSb.ToString());


            }
            return queryable;
        }
    }
    
}
