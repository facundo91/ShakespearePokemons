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
            services.AddHealthChecks();
            var ShakespeareSettings = Configuration.GetSetting<ShakespeareSettings>();

            services.AddHttpClient<IShakespeareClient, ShakespeareClient>(client =>
            {
                client.BaseAddress = ShakespeareSettings.BaseUri;
            })
            .AddHttpMessageHandler<SimpleExceptionsHandler>()
            .AddPolicyHandler(GetCircuitBreakerPolicy());
            services.AddTransient<SimpleExceptionsHandler>();
            services.AddSingleton<IPokemonClient, PokemonClient>();
            services.AddTransient<IPokemonService, PokemonService>();
        }

        static IAsyncPolicy<HttpResponseMessage> GetCircuitBreakerPolicy()
        {
            return HttpPolicyExtensions
                .HandleTransientHttpError()
                .OrResult(msg => msg.StatusCode == System.Net.HttpStatusCode.TooManyRequests)
                .CircuitBreakerAsync(5, TimeSpan.FromMinutes(5),
                  (ex, time) => Console.WriteLine($"Circuit broken. Will be open in {time.TotalSeconds} seconds."),
                  () => Console.WriteLine("Circuit Reset."));
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, IApiVersionDescriptionProvider provider)
        {
            if (env.IsDevelopment())
                app.UseDeveloperExceptionPage();

            app.UseHttpsRedirection();

            app.UseRouting();
            app.UseEndpoints(builder =>
            {
                builder.MapControllers();
                builder.MapHealthChecks("/health");
            });
            app.UserSwagger(provider);
        }
    }

}
