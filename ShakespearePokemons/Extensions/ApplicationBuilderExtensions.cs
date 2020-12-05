using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Newtonsoft.Json;
using System.Linq;
using ShakespearePokemons.Contracts.HealthChecks;
using ShakespearePokemons.Contracts;

namespace ShakespearePokemons.Extensions
{
    public static class ApplicationBuilderExtensions
    {
        public static IApplicationBuilder UserSwagger(this IApplicationBuilder app, IApiVersionDescriptionProvider provider)
        {
            app.UseSwagger();
            app.UseSwaggerUI(
                options =>
                {
                    options.RoutePrefix = string.Empty;
                    // build a swagger endpoint for each discovered API version
                    foreach (var description in provider.ApiVersionDescriptions)
                        options.SwaggerEndpoint($"/swagger/{description.GroupName}/swagger.json",
                            description.GroupName.ToUpperInvariant());
                });
            return app;
        }

        public static void AddHealthChecks(this IApplicationBuilder app)
        {
            app.UseHealthChecks($"/{ApiRoutes.Health}", new HealthCheckOptions
            {
                ResponseWriter = async (context, report) =>
                {
                    context.Response.ContentType = "application/json";

                    var response = new HealthCheckResponse
                    {
                        Status = report.Status.ToString(),
                        Checks = report.Entries.Select(_ => new HealthCheck
                        {
                            Component = _.Key,
                            Status = _.Value.Status.ToString(),
                            Description = _.Value.Description
                        }),
                        Duration = report.TotalDuration
                    };

                    await context.Response.WriteAsync(JsonConvert.SerializeObject(response));
                }
            });
        }
    }
}
