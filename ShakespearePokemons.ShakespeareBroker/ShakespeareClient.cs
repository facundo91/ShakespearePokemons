using Flurl;
using System.Net.Http;
using System.Threading.Tasks;

namespace ShakespearePokemons.ShakespeareBroker
{
    public class ShakespeareClient : IShakespeareClient
    {
        private readonly HttpClient _httpClient;

        public ShakespeareClient(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<ShakespeareApiResponse> GetTranslationAsync(string textToTranslate)
        {
            var url = _httpClient.BaseAddress
                .AppendPathSegment("shakespeare")
                .SetQueryParam("text", textToTranslate);
            var result = await _httpClient.GetAsync(url);
            return await result.Content.ReadAsAsync<ShakespeareApiResponse>();
        }
    }
}
