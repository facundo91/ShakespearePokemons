using System.Threading.Tasks;

namespace ShakespearePokemons.Services.Interfaces
{
    public interface IPokemonService
    {
        public Task<string> GetPokemonDescriptionAsShakespeareAsync(string pokemonName);
    }
}
