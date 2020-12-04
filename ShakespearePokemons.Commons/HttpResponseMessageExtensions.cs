using System.Net.Http;
using System.Threading.Tasks;

namespace ShakespearePokemons.Commons
{
    public static class HttpResponseMessageExtensions
    {
        public static async Task EnsureSuccessStatusCodeAsync(this HttpResponseMessage response)
        {
            try
            {
                response.EnsureSuccessStatusCode();
                return;
            }
            catch (HttpRequestException exception)
            {
                var content = await response.Content.ReadAsStringAsync();

                if (response.Content != null)
                    response.Content.Dispose();

                throw new SimpleHttpResponseException(response.StatusCode, content, exception);
            }
        }
    }
}
