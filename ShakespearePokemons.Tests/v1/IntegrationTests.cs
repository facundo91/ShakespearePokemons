using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Moq;
using ShakespearePokemons.Commons;
using ShakespearePokemons.PokemonBroker;
using ShakespearePokemons.ShakespeareBroker;
using System;
using System.Net;
using System.Net.Http;

namespace ShakespearePokemons.Tests.v1
{
    public class IntegrationTests : IDisposable
    {
        protected Mock<IPokemonClient> pokemonClientMock;
        protected Mock<IShakespeareClient> shakespeareClientMock;
        public readonly HttpClient TestClient;

        protected IntegrationTests()
        {
            SetUpPokemonClientMock();
            SetUpShakespeareClientMock();
            var appFactory = new WebApplicationFactory<Startup>().WithWebHostBuilder(builder =>
            {
                builder.ConfigureServices(services =>
                {
                    services.RemoveAll<IPokemonClient>();
                    services.RemoveAll<IShakespeareClient>();
                    services.TryAddSingleton<IPokemonClient>(pokemonClientMock.Object);
                    services.TryAddSingleton<IShakespeareClient>(shakespeareClientMock.Object);
                });
            });
            TestClient = appFactory.CreateClient();
        }

        private void SetUpShakespeareClientMock()
        {
            shakespeareClientMock = new Mock<IShakespeareClient>();
            ShakespeareApiResponse shakespeareApiResponse = new()
            {
                Contents = new() { Translated = Constants.translatedDescription }
            };
            shakespeareClientMock
                .Setup(client => client.GetTranslationAsync(Constants.description))
                .ReturnsAsync(shakespeareApiResponse);
        }

        private void SetUpPokemonClientMock()
        {
            pokemonClientMock = new Mock<IPokemonClient>();
            pokemonClientMock
                .Setup(client => client.GetPokemonDescriptionAsync(Constants.pikachu, Constants.eng))
                .ReturnsAsync(Constants.description);
            pokemonClientMock
            .Setup(client => client.GetPokemonDescriptionAsync(Constants.unexistingPokemon, Constants.eng))
            .ThrowsAsync(new SimpleHttpResponseException(HttpStatusCode.NotFound, Constants.unexistingPokemon));
        }


        public void Dispose()
        {
        }
    }
}
