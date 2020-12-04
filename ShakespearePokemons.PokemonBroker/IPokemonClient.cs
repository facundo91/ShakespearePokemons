using System.Threading.Tasks;

namespace ShakespearePokemons.PokemonBroker
{
    public interface IPokemonClient
    {
        Task<string> GetPokemonDescriptionAsync(string pokemonName, string language = "en");
    }
}