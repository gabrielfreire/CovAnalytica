using CovAnalytica.Shared.Interfaces;
using CovAnalytica.Shared.Models;
using Microsoft.AspNetCore.Mvc;

namespace CovAnalytica.Server.Controllers
{
    [ApiController]
    [Route("data")]
    public class CovidController : ControllerBase
    {
        private IQueryService _queryService;

        public CovidController(IQueryService queryService)
        {
            _queryService = queryService;
        }

        [HttpGet]
        [Produces("application/json")]
        [Route("timeseries-covid-data")]
        public async Task<ActionResult> ListTimeseriesCovidData(
            [FromQuery] string? Continent,
            [FromQuery] string? Location,
            [FromQuery] int? skip = 0,
            [FromQuery] int? count = 0
            )
        {
            var _queryParams = QueryParams.FromQueries(GetQueriesForSearch());
            return Ok(await _queryService.ListTimeseriesDataWithQueryParamsAsync(_queryParams, false));
        }
        [HttpGet]
        [Produces("application/json")]
        [Route("totals-per-country")]
        public async Task<ActionResult> ListTotalsPerCountryCovidData(
            [FromQuery] string? Continent,
            [FromQuery] string? Location,
            [FromQuery] int? skip = 0,
            [FromQuery] int? count = 0
            )
        {
            var _queryParams = QueryParams.FromQueries(GetQueriesForSearch());
            return Ok(await _queryService.ListTotalsPerCountryWithQueryParamsAsync(_queryParams, false));
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
