using Polly;
using Polly.Extensions.Http;
using ShakespearePokemons.HealthChecks;
using System;
using System.Net.Http;

namespace ShakespearePokemons.Policies
{
    public static class CircuitBreakerPolicy
    {
        internal static IAsyncPolicy<HttpResponseMessage> GetPolicyAsync() =>
            HttpPolicyExtensions
                .HandleTransientHttpError()
                .OrResult(msg => msg.StatusCode == System.Net.HttpStatusCode.TooManyRequests)
                .CircuitBreakerAsync(5, TimeSpan.FromMinutes(5),
                  (ex, time) =>
                  {
                      Console.WriteLine($"Circuit broken. Will be open in {time.TotalSeconds} seconds.");
                      ShakespeareHealthCheck.CircuitClosed = false;
                      ShakespeareHealthCheck.ChangeTime = DateTime.Now;
                  },
                  () =>
                  {
                      Console.WriteLine("Circuit Reset.");
                      ShakespeareHealthCheck.CircuitClosed = true;
                      ShakespeareHealthCheck.ChangeTime = DateTime.Now;
                  });
    }
}
