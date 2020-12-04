using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using ShakespearePokemons.Swagger;
using Swashbuckle.AspNetCore.SwaggerGen;
using System;
using System.IO;
using System.Linq;

namespace ShakespearePokemons.Extensions
{
    public static class ServiceCollectionSwaggerExtension
    {
        public static IServiceCollection AddSwaggerWithApiVersioning(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddApiVersioning(
                options =>
                {
                    // reporting api versions will return the headers "api-supported-versions" and "api-deprecated-versions"
                    options.ReportApiVersions = true;
                });
            services.AddVersionedApiExplorer(
                options =>
                {
                    // add the versioned api explorer, which also adds IApiVersionDescriptionProvider service
                    // note: the specified format code will format the version as "'v'major[.minor][-status]"
                    options.GroupNameFormat = "'v'VVV";

                    // note: this option is only necessary when versioning by url segment. the SubstitutionFormat
                    // can also be used to control the format of the API version in route templates
                    options.SubstituteApiVersionInUrl = true;
                });
            services.AddTransient<IConfigureOptions<SwaggerGenOptions>, ConfigureSwaggerOptions>();
            services.AddSwaggerGen(
                options =>
                {
                    // add a custom operation filter which sets default values
                    options.OperationFilter<SwaggerDefaultValues>();

                    // integrate xml comments
                    var files = Directory.GetFiles(AppContext.BaseDirectory, "*.xml", SearchOption.TopDirectoryOnly)
                        .ToList();
                    files.ForEach(xmlFile => options.IncludeXmlComments(xmlFile));

                    //options.IncludeXmlComments(XmlCommentsFilePath);
                });
            return services;
        }
    }
}
