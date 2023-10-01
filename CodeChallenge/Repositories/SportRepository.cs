using CodeChallenge.Common;
using CodeChallenge.Configuration;
using CodeChallenge.Domain.Entities;
using Microsoft.Azure.Cosmos;

namespace CodeChallenge.Repositories
{
    public class SportRepository : CosmosBaseRepository, ISportRepository
    {
        private readonly Container _sportContainer;

        private static QueryDefinition QueryBySportId(string type, string sportId) =>
          new QueryDefinition($"SELECT TOP 1 * FROM p WHERE p.sportId = '{sportId}' and p.type = '{type}'");

        public SportRepository(CosmosClient client, IAppSettings appSettings)
        {
            var databaseName = appSettings.CosmosDb.DatabaseName;
            _sportContainer = client.GetContainer(databaseName, Constants.ContainerName);
        }

        public async Task<SportEntity> GetSport(string sportId)
        {
            var query = QueryBySportId(EntityType.Sport.ToString(), sportId);

            return await GetItem<SportEntity>(_sportContainer, sportId, query);
        }
    }
}
