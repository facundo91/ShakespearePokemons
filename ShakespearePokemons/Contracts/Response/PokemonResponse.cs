namespace ShakespearePokemons.Contracts.Response
{
    /// <summary> Simple Pokemon response </summary>
    public sealed record PokemonResponse(
        /// <summary> Name of the Pokemon </summary>
        string Name,
        /// <summary> Description of the Pokemon </summary>
        string Description);
}
