using FluentAssertions;
using Moq;
using Refit;
using ShakespearePokemons.Client;
using ShakespearePokemons.Commons;
using System.Net;
using System.Threading.Tasks;
using Xunit;

namespace ShakespearePokemons.Tests.v1
{
    public class PokemonTests : IntegrationTests
    {
        private readonly IPokemons _pokemonsApi;

        public PokemonTests()
        {
            _pokemonsApi = RestService.For<IPokemons>(TestClient);
        }

        [Fact]
        public async Task GetTranslationAsync_HappyPathScenario_TranslatedString()
        {
            // arrange
            // act
            var response = await _pokemonsApi.GetTranslationAsync(Constants.pikachu);
            // assert
            response.StatusCode.Should().Be(HttpStatusCode.OK);
            response.Content.Description.Should().Be(Constants.translatedDescription);
        }

        [Fact]
        public async Task GetTranslationAsync_EmptyString_StatusCodeNotFound()
        {
            // arrange
            // act
            var response = await _pokemonsApi.GetTranslationAsync("");
            // assert
            response.StatusCode.Should().Be(HttpStatusCode.NotFound);
        }

        [Fact]
        public async Task GetTranslationAsync_WhiteSpaceString_StatusCodeBadRequest()
        {
            // arrange
            // act
            var response = await _pokemonsApi.GetTranslationAsync("  ");
            // assert
            response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        }

        [Fact]
        public async Task GetTranslationAsync_PokemonNotFound_StatusCodeNotFound()
        {
            // arrange
            // act
            var response = await _pokemonsApi.GetTranslationAsync(Constants.unexistingPokemon);
            // assert
            response.StatusCode.Should().Be(HttpStatusCode.NotFound);
        }

        [Fact]
        public async Task GetTranslationAsync_ShakespeareApiLimitReached_StatusCodeTooManyRequests()
        {
            // arrange
            shakespeareClientMock
                .Setup(client => client.GetTranslationAsync(Constants.description))
                .ThrowsAsync(new SimpleHttpResponseException((HttpStatusCode)429, "Too Many Requests"));
            // act
            var response = await _pokemonsApi.GetTranslationAsync(Constants.pikachu);
            // assert
            response.StatusCode.Should().Be(429);
        }
    }
}
