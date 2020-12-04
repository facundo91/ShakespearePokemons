using System;
using System.Net.Http;
using System.Web;
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
            var endpoint = new Uri(_httpClient.BaseAddress, "shakespeare");
            var builder = new UriBuilder(endpoint);
            var query = HttpUtility.ParseQueryString(builder.Query);
            query["text"] = textToTranslate;
            builder.Query = query.ToString();
            var result = await _httpClient.GetAsync(builder.ToString());
            return await result.Content.ReadAsAsync<ShakespeareApiResponse>();
        }
    }
}
