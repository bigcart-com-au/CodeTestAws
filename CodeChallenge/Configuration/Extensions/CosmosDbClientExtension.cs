using Microsoft.Azure.Cosmos.Fluent;
using Microsoft.Azure.Cosmos;

namespace CodeChallenge.Configuration.Extensions
{
    public static class CosmosDbClientExtension
    {
        public static void AddCosmosDbClient(this IServiceCollection services, IAppSettings appSettings)
        {
            var client = new CosmosClientBuilder(appSettings.CosmosDb.AccountEndpoint, appSettings.CosmosDb.PrimaryKey)
                .WithSerializerOptions(new CosmosSerializationOptions
                {
                    PropertyNamingPolicy = CosmosPropertyNamingPolicy.CamelCase
                })
                .WithConnectionModeDirect(TimeSpan.FromMinutes(20),
                    null, null, null,
                    PortReuseMode.PrivatePortPool)
                .WithContentResponseOnWrite(false)
                .Build();

            services.AddSingleton(client);
        }
    }
}
