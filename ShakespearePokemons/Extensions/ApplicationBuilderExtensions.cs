using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Mvc.ApiExplorer;

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
    }
}
