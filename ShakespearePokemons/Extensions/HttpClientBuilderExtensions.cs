using Microsoft.Extensions.DependencyInjection;
using ShakespearePokemons.Policies;

namespace ShakespearePokemons.Extensions
{
    public static class HttpClientBuilderExtensions
    {
        public static IHttpClientBuilder AddCircuitBreakerPolicy(this IHttpClientBuilder httpClientBuilder) =>
            httpClientBuilder.AddPolicyHandler(CircuitBreakerPolicy.GetPolicyAsync());
    }
}
