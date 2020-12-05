using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Polly.CircuitBreaker;
using ShakespearePokemons.Commons;
using ShakespearePokemons.Contracts;
using ShakespearePokemons.Contracts.Response;
using ShakespearePokemons.Services.Interfaces;
using System.Threading.Tasks;
using static Microsoft.AspNetCore.Http.StatusCodes;
using static ShakespearePokemons.Contracts.ApiVersions;

namespace ShakespearePokemons.Controllers.v1
{
    /// <summary> Resource for Pokemons. </summary>
    [ApiVersion(V1Tag)]
    [ApiController]
    public class PokemonController : ControllerBase
    {
        private readonly IPokemonService _pokemonService;

        public PokemonController(IPokemonService pokemonService)
        {
            _pokemonService = pokemonService;
        }

        /// <summary>
        /// Endpoint to get the description of the Pokemon as William Shakespeare would have said it. 
        /// </summary>
        /// <param name="pokemonName">Name of the pokemon.</param>
        /// <param name="apiBehaviorOptions">Parameter bound by using FromServices attribute.</param>
        /// <returns>A simple <see cref="PokemonResponse"/> object response.</returns>
        /// <response code="200">Returns the translated description of the Pokemon.</response>
        /// <response code="404">Pokemon not found.</response>
        /// <response code="400">Something about <paramref name="pokemonName"/> is not right.</response>
        /// <response code="429">This could be probably due to the limit of the Shakespeare Api.</response>
        /// <response code="403">The Circuit is open, you should wait.</response>
        [ProducesResponseType(typeof(PokemonResponse), Status200OK)]
        [HttpGet(ApiRoutes.Pokemon.GetTranslation)]
        public async Task<IActionResult> GetTranslation(
            [FromRoute] string pokemonName,
            [FromServices] IOptions<ApiBehaviorOptions> apiBehaviorOptions)
        {
            if (string.IsNullOrWhiteSpace(pokemonName))
            {
                var errorResponse = new ErrorResponse(Status400BadRequest, "Pokemon's name cannot be null, empty nor white space.");
                return BadRequest(errorResponse);
            }
            try
            {
                var shakespeareDescriptionTranslation = await _pokemonService.GetPokemonDescriptionAsShakespeareAsync(pokemonName);
                var pokemonResponse = new PokemonResponse
                {
                    Name = pokemonName,
                    Description = shakespeareDescriptionTranslation
                };
                return Ok(pokemonResponse);
            }
            catch (BrokenCircuitException)
            {
                var errorResponse = new ErrorResponse(Status403Forbidden, "Shakespeare API is inoperative, please try later on. Circuit is now open.");
                return StatusCode(Status403Forbidden, errorResponse);
            }
            catch (SimpleHttpResponseException simpleHttpResponseException)
            {
                var errorResponse = new ErrorResponse(simpleHttpResponseException.StatusCode, simpleHttpResponseException.Message);
                return StatusCode(simpleHttpResponseException.StatusCodeValue, errorResponse);
            }
        }
    }
}
