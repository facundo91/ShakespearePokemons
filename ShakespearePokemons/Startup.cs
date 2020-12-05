using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using ShakespearePokemons.Extensions;
using ShakespearePokemons.PokemonBroker;
using ShakespearePokemons.Services.Implementations;
using ShakespearePokemons.Services.Interfaces;
using ShakespearePokemons.ShakespeareBroker;
using Polly;
using Polly.Extensions.Http;
using System;
using System.Net.Http;
using Polly.CircuitBreaker;
using ShakespearePokemons.Commons;
using ShakespearePokemons.HealthChecks;

namespace ShakespearePokemons
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();
            services.AddSwaggerWithApiVersioning(Configuration);
            services.AddHealthChecks().AddCheck<ShakespeareHealthCheck>(nameof(ShakespeareHealthCheck));
            var ShakespeareSettings = Configuration.GetSetting<ShakespeareSettings>();

            services
                .AddHttpClient<IShakespeareClient, ShakespeareClient>
                    (client => { client.BaseAddress = ShakespeareSettings.BaseUri; })
                .AddHttpMessageHandler<SimpleExceptionsHandler>()
                .AddCircuitBreakerPolicy();

            services.AddTransient<SimpleExceptionsHandler>();
            services.AddSingleton<IPokemonClient, PokemonClient>();
            services.AddTransient<IPokemonService, PokemonService>();
            services.AddSingleton<ShakespeareHealthCheck>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, IApiVersionDescriptionProvider provider)
        {
            if (env.IsDevelopment())
                app.UseDeveloperExceptionPage();

            app.UseHttpsRedirection();

            app.AddHealthChecks();
            app.UseRouting();
            app.UseEndpoints(builder =>
            {
                builder.MapControllers();
            });
            app.UserSwagger(provider);
        }
    }

}
