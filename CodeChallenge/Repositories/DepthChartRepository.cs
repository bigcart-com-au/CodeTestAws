using CodeChallenge.Common;
using CodeChallenge.Configuration;
using CodeChallenge.Domain.Entities;
using Microsoft.Azure.Cosmos;

namespace CodeChallenge.Repositories
{
    public class DepthChartRepository : CosmosBaseRepository, IDepthChartRepository
    {
        private readonly Container _depthChartContainer;

        private static QueryDefinition QueryBySportAndPosition(string type, string sportId, string position) => 
            new QueryDefinition($"SELECT TOP 1 * FROM p WHERE p.sportId = '{sportId}' and p.type = '{type}' and p.position = '{position}'");

        private static QueryDefinition QueryBySport(string type, string sportId) =>
            new QueryDefinition($"SELECT * FROM p WHERE p.sportId = '{sportId}' and p.type = '{type}'");

        public DepthChartRepository(CosmosClient client, IAppSettings appSettings)
        {
            var databaseName = appSettings.CosmosDb.DatabaseName;
            _depthChartContainer = client.GetContainer(databaseName, Constants.ContainerName);
        }

        public async Task<DepthChartEntity> AddDepthChart(DepthChartEntity depthChartEntity)
        {
            var response = await _depthChartContainer.CreateItemAsync(
                depthChartEntity,
                new PartitionKey(depthChartEntity.SportId),
                new ItemRequestOptions { EnableContentResponseOnWrite = true });

            return response.Resource;
        }

        public async Task<DepthChartEntity> GetDepthChart(string sportId, string position)
        {
            var query = QueryBySportAndPosition(EntityType.DepthChart.ToString(), sportId, position);

            return await GetItem<DepthChartEntity>(_depthChartContainer, sportId, query);
        }

        public async Task<IEnumerable<DepthChartEntity>> GetDepthCharts(string sportId)
        {
            var query = QueryBySport(EntityType.DepthChart.ToString(), sportId);

            return await GetAll<DepthChartEntity>(_depthChartContainer, sportId, query);
        }

        public Task UpdateDepthChart(DepthChartEntity depthChartEntity)
        {
            return _depthChartContainer.PatchItemAsync<DepthChartEntity>(
                    id: depthChartEntity.Id.ToString(),
                    partitionKey: new PartitionKey(depthChartEntity.SportId),
                    patchOperations: new[] {
                        PatchOperation.Replace("/rankedPlayerIds", depthChartEntity.RankedPlayerIds)
                    }
                );
        }
    }
}
