using PokeApiNet;
using System.Linq;
using System.Text.RegularExpressions;

namespace ShakespearePokemons.PokemonBroker
{
    public static class PokemonHelper
    {
        public static string GetEscapedDescriptionFromPokemonSpecies(PokemonSpecies pokemonSpecies, string language)
        {
            var description = pokemonSpecies.FlavorTextEntries.FirstOrDefault(flavor => flavor.Language.Name == language).FlavorText;
            return Regex.Escape(description).Replace("\\n", " ").Replace("\\", " ").Replace("  ", " ");
        }
    }
}
