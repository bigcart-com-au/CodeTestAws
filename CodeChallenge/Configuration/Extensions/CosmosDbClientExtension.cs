using Microsoft.Azure.Cosmos.Fluent;
using Microsoft.Azure.Cosmos;

namespace CodeChallenge.Configuration.Extensions
{
    public static class CosmosDbClientExtension
    {
        private static readonly string CosmosPrimaryKey = "EJr6onUmz1KeSDBAbeVD2jBiVN1DX5sm2KvKJYPxWOsXMd6I8NHhyoiyK04jQP3nXF8xiAo1evaoACDbjAdvEA==";

        public static void AddCosmosDbClient(this IServiceCollection services, IAppSettings appSettings)
        {
            var client = new CosmosClientBuilder(appSettings.CosmosDb.AccountEndpoint, CosmosPrimaryKey)
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
