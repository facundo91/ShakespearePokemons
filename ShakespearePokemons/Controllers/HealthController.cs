using Microsoft.AspNetCore.Mvc;
using ShakespearePokemons.Contracts;
using ShakespearePokemons.Contracts.HealthChecks;
using System.Net;
using static ShakespearePokemons.Contracts.ApiVersions;

namespace ShakespearePokemons.Controllers
{
    [ApiVersionNeutral]
    [ApiController]
    public class HealthController : ControllerBase
    {

        /// <summary>
        ///     Get Health
        /// </summary>
        /// <remarks>Provides an indication about the health of the API</remarks>
        /// <response code="200">API is healthy</response>
        /// <response code="503">API is unhealthy or in degraded state</response>
        [HttpGet(ApiRoutes.Health)]
        [ProducesResponseType(typeof(HealthCheckResponse), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(HealthCheckResponse), (int)HttpStatusCode.ServiceUnavailable)]
        public IActionResult GetHealth()
        {
            return Ok();
        }
    }
}
