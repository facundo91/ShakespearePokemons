using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace ShakespearePokemons.Commons
{
    public class SimpleExceptionsHandler : DelegatingHandler
    {
        protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            if (InnerHandler is null) InnerHandler = new HttpClientHandler();
            var response = await base.SendAsync(request, cancellationToken);
            await response.EnsureSuccessStatusCodeAsync();
            return response;
        }
    }
}