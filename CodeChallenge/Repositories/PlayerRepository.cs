using CodeChallenge.Common;
using CodeChallenge.Configuration;
using CodeChallenge.Domain.Entities;
using Microsoft.Azure.Cosmos;

namespace CodeChallenge.Repositories
{
    public class PlayerRepository : CosmosBaseRepository, IPlayerRepository
    {
        private readonly Container _playerContainer;

        private static QueryDefinition QueryByPlayer(string type, string sportId, int playerId) =>
           new QueryDefinition($"SELECT TOP 1 * FROM p WHERE p.sportId = '{sportId}' and p.type = '{type}' and p.playerId = '{playerId}'");

        public PlayerRepository(CosmosClient client, IAppSettings appSettings)
        {
            var databaseName = appSettings.CosmosDb.DatabaseName;
            _playerContainer = client.GetContainer(databaseName, Constants.ContainerName);
        }

        public async Task<PlayerEntity> AddPlayer(PlayerEntity player)
        {
            var response = await _playerContainer.CreateItemAsync(
                player,
                new PartitionKey(player.SportId),
                new ItemRequestOptions { EnableContentResponseOnWrite = true });

            return response.Resource;
        }

        public async Task<PlayerEntity> GetPlayer(string sportId, int playerId)
        {
            var query = QueryByPlayer(EntityType.Player.ToString(), sportId, playerId);

            return await GetItem<PlayerEntity>(_playerContainer, sportId, query);
        }
    }
}
