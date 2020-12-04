using ShakespearePokemons.PokemonBroker;
using ShakespearePokemons.Services.Interfaces;
using ShakespearePokemons.ShakespeareBroker;
using System.Threading.Tasks;

namespace ShakespearePokemons.Services.Implementations
{
    public class PokemonService : IPokemonService
    {
        private readonly IShakespeareClient _shakespeareClient;
        public readonly IPokemonClient _pokemonClient;

        public PokemonService(IShakespeareClient shakespeareClient, IPokemonClient pokemonClient)
        {
            _shakespeareClient = shakespeareClient;
            _pokemonClient = pokemonClient;
        }

        public async Task<string> GetPokemonDescriptionAsShakespeareAsync(string pokemonName)
        {
            var pokemonDescription = await _pokemonClient.GetPokemonDescriptionAsync(pokemonName);
            var shakespeareResponse = await _shakespeareClient.GetTranslationAsync(pokemonDescription);
            return shakespeareResponse.Contents.Translated;
        }
    }
}
