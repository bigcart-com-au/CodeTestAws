using System.Text.Json.Serialization;
using System.Text.Json;

namespace CodeChallenge.Configuration
{
    public static class JsonOptionsFactory
    {
        public static JsonSerializerOptions DefaultSerializerOptions
        {
            get
            {
                var serializerOptions = new JsonSerializerOptions();
                serializerOptions.ApplyDefaultConfig();
                return serializerOptions;
            }
        }
        public static void ApplyDefaultConfig(this JsonSerializerOptions options)
        {
            options.DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull;
            options.PropertyNamingPolicy = JsonNamingPolicy.CamelCase;
            options.WriteIndented = true;
            options.PropertyNameCaseInsensitive = true;
            options.Converters.Add(new JsonStringEnumConverter());
        }
    }
}
