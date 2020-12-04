using System.Threading.Tasks;

namespace ShakespearePokemons.ShakespeareBroker
{
    public interface IShakespeareClient
    {
        Task<ShakespeareApiResponse> GetTranslationAsync(string textToTranslate);
    }
}
