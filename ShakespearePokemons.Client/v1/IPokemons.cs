using Refit;
using System.Threading.Tasks;
using ShakespearePokemons.Contracts.Response;

namespace ShakespearePokemons.Client
{
    public interface IPokemons
    {

        [Get("/api/pokemon/{pokemonName}?api-version=1.0")]
        Task<ApiResponse<PokemonResponse>> GetTranslationAsync(string pokemonName);
    }
}
