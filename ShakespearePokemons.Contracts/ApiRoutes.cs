namespace ShakespearePokemons.Contracts
{
    public static class ApiRoutes
    {
        private const string Base = "api";

        public static class Pokemon
        {
            private const string Relative = Base + "/pokemon";
            public const string GetTranslation = Relative + "/{pokemonName}";
        }

        public const string Health = Base + "/health";

    }
}
