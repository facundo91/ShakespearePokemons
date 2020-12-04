using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using ShakespearePokemons.Contracts;
using ShakespearePokemons.Contracts.Response;
using System.Threading.Tasks;
using static Microsoft.AspNetCore.Http.StatusCodes;
using static ShakespearePokemons.Contracts.ApiVersions;

namespace ShakespearePokemons.Controllers
{
    [Consumes("application/json")]
    [Produces("application/json")]
    [ApiVersion(V1Tag)]
    [ProducesResponseType(typeof(PokemonResponse), Status200OK)]
    [ProducesResponseType(Status400BadRequest)]
    [ApiController]
    public class PokemonController : ControllerBase
    {
        [HttpGet(ApiRoutes.Pokemon.GetTranslation)]
        public async Task<IActionResult> GetTranslation(
            [FromRoute] string pokemonName, 
            [FromServices] IOptions<ApiBehaviorOptions> apiBehaviorOptions)
        {
            if (string.IsNullOrWhiteSpace(pokemonName))
            {
                ModelState.AddModelError("Pokemon Name", "Pokemon's name cannot be null, empty nor white space.");
                return apiBehaviorOptions.Value.InvalidModelStateResponseFactory(ControllerContext);
            }

            return Ok(new PokemonResponse(pokemonName, "Fake description"));
        }
    }
}
