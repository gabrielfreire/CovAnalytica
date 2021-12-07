using CovAnalytica.Shared.Interfaces;
using CovAnalytica.Shared.Models;
using Microsoft.AspNetCore.Mvc;

namespace CovAnalytica.Server.Controllers
{
    [ApiController]
    [Route("data")]
    public class DataController : ControllerBase
    {
        private ICovidDataQueryService _covidQueryService;
        private IVaersDataQueryService _vaersDataQueryService;

        public DataController(
            ICovidDataQueryService queryService, 
            IVaersDataQueryService vaersDataQueryService)
        {
            _covidQueryService = queryService;
            _vaersDataQueryService = vaersDataQueryService;
        }

        [HttpGet]
        [Produces("application/json")]
        [Route("timeseries-covid-data")]
        public async Task<ActionResult> ListTimeseriesCovidData(
            [FromQuery] string? selects,
            [FromQuery] string? Continent,
            [FromQuery] string? Location,
            [FromQuery] int? skip = 0,
            [FromQuery] int? count = 0
            )
        {
            var _queryParams = QueryParams.FromQueries(GetQueriesForSearch());
            return Ok(await _covidQueryService.ListTimeseriesDataWithQueryParamsAsync(_queryParams, false));
        }

        [HttpGet]
        [Produces("application/json")]
        [Route("countries")]
        public async Task<ActionResult> ListAllCountries()
        {
            return Ok(await _covidQueryService.ListAllCountriesAsync());
        }

        [HttpGet]
        [Produces("application/json")]
        [Route("continents")]
        public async Task<ActionResult> ListAllContinents()
        {
            return Ok(await _covidQueryService.ListAllContinentsAsync());
        }

        [HttpGet]
        [Produces("application/json")]
        [Route("vaers-vax-adverse-events")]
        public async Task<ActionResult> ListVaersVaxAdverseEventsData(
            [FromQuery] string? selects,
            [FromQuery] string? vaccineType,
            [FromQuery] string? vaccineManufacturer,
            [FromQuery] string? vaccineDose,
            [FromQuery] string? eventCategory,
            [FromQuery] string? ageCode,
            [FromQuery] int? skip = 0,
            [FromQuery] int? count = 0
            )
        {
            var _queryParams = QueryParams.FromQueries(GetQueriesForSearch());
            return Ok(await _vaersDataQueryService.ListVaxAdverseEventsWithQueryParamsAsync(_queryParams, false));
        }

        [HttpGet]
        [Produces("application/json")]
        [Route("vaers-vax-adverse-events/event-categories")]
        public async Task<ActionResult> ListAdverseEventsCategories()
        {
            return Ok(await _vaersDataQueryService.ListEventCategoriesAsync());
        }
        [HttpGet]
        [Produces("application/json")]
        [Route("vaers-vax-adverse-events/vaccine-types")]
        public async Task<ActionResult> ListVaccineTypes()
        {
            return Ok(await _vaersDataQueryService.ListVaccineTypesAsync());
        }
        [HttpGet]
        [Produces("application/json")]
        [Route("vaers-vax-adverse-events/vaccine-manufacturers")]
        public async Task<ActionResult> ListVaccineManufacturers()
        {
            return Ok(await _vaersDataQueryService.ListVaccineManufacturersAsync());
        }
        [HttpGet]
        [Produces("application/json")]
        [Route("vaers-vax-adverse-events/age-groups")]
        public async Task<ActionResult> ListAgeGroups()
        {
            return Ok(await _vaersDataQueryService.ListVaccineAgeGroupsAsync());
        }

        private IReadOnlyList<Tuple<string, string>> GetQueriesForSearch()
        {
            var queries = Array.Empty<Tuple<string, string>>();

            if (Request.Query != null)
            {
                queries = Request.Query
                    .SelectMany(query => query.Value, (query, value) => Tuple.Create(query.Key, value)).ToArray();
            }

            return queries;
        }
    }
}
