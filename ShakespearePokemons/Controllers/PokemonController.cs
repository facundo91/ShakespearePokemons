using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using ShakespearePokemons.Contracts;
using ShakespearePokemons.Contracts.Response;
using ShakespearePokemons.Services.Interfaces;
using System.Threading.Tasks;
using static Microsoft.AspNetCore.Http.StatusCodes;
using static ShakespearePokemons.Contracts.ApiVersions;

namespace ShakespearePokemons.Controllers
{
    [ApiVersion(V1Tag)]
    [ApiController]
    public class PokemonController : ControllerBase
    {
        private readonly IPokemonService _pokemonService;

        public PokemonController(IPokemonService pokemonService)
        {
            _pokemonService = pokemonService;
        }


        [ProducesResponseType(typeof(PokemonResponse), Status200OK)]
        [ProducesResponseType(Status400BadRequest)]
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
            var shakespeareDescriptionTranslation = await _pokemonService.GetPokemonDescriptionAsShakespeareAsync(pokemonName);

            return Ok(new PokemonResponse(pokemonName, shakespeareDescriptionTranslation));
        }
    }
}
