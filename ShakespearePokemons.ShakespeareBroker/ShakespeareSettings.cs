using System;

namespace ShakespearePokemons.ShakespeareBroker
{
    public sealed class ShakespeareSettings
    {
        public string BaseUrl { get; set; }
        public Uri BaseUri => new Uri(BaseUrl);
    };
}
