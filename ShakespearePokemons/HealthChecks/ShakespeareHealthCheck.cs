using Microsoft.Extensions.Diagnostics.HealthChecks;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace ShakespearePokemons.HealthChecks
{
    public class ShakespeareHealthCheck : IHealthCheck
    {
        public static bool CircuitClosed { get; set; } = true;
        public static DateTime ChangeTime { get; set; } = DateTime.Now;
        public Task<HealthCheckResult> CheckHealthAsync(HealthCheckContext context, CancellationToken cancellationToken = default)
        {
            var timeSinceLastChange = (DateTime.Now - ChangeTime).Minutes;
            return Task.FromResult(
                CircuitClosed ? 
                HealthCheckResult.Healthy($"The Service is working fine since {timeSinceLastChange}' ago.") : 
                HealthCheckResult.Unhealthy($"The limit have been reach {timeSinceLastChange}' ago, wait to try again."));
        }
    }
}
