using System.Threading.Tasks;
using PokeApiNet;

namespace ShakespearePokemons.PokemonBroker
{
    public class PokemonClient : IPokemonClient
    {
        readonly PokeApiClient _pokeClient;
        public PokemonClient()
        {
            _pokeClient = new PokeApiClient();
        }

        private async Task<PokemonSpecies> GetPokemonAsync(string pokemonName) =>
            await _pokeClient.GetResourceAsync<PokemonSpecies>(pokemonName);

        public async Task<string> GetPokemonDescriptionAsync(string pokemonName, string language)
        {
            var pokemonSpecies = await GetPokemonAsync(pokemonName);
            return PokemonHelper.GetEscapedDescriptionFromPokemonSpecies(pokemonSpecies, language);
        }

    }
}
