using Microsoft.Extensions.Configuration;

namespace ShakespearePokemons.Extensions
{
    public static class ConfigurationExtensions
    {
        public static T GetSetting<T>(this IConfiguration configuration) where T : class, new()
        {
            T setting = new();
            configuration.Bind(typeof(T).Name, setting);
            return setting;
        }
    }
}
