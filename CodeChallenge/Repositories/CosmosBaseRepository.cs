using Microsoft.Azure.Cosmos;

namespace CodeChallenge.Repositories
{
    public abstract class CosmosBaseRepository
    {
        private const int DefaultItemCount = -1;

        protected async Task<T> GetItem<T>(
            Container container, 
            string partitionKey,
            QueryDefinition queryDefinition)
            where T : class
        {
            using FeedIterator<T> queryIterator = container.GetItemQueryIterator<T>(
                queryDefinition,
            null,
            new QueryRequestOptions
                {
                    PartitionKey = new PartitionKey(partitionKey),
                    MaxConcurrency = -1,
                    MaxBufferedItemCount = 1,
                    MaxItemCount = 1
                });

            while (queryIterator is { HasMoreResults: true })
            {
                var response = await queryIterator.ReadNextAsync();
                return response.Resource.FirstOrDefault();
            }

            return null;
        }

        protected async Task<IEnumerable<T>> GetAll<T>(
            Container container, 
            string partitionKey,
            QueryDefinition queryDefinition, 
            int maxItemCount = DefaultItemCount)
            where T : class
        {
            var resources = new List<T>();
            using FeedIterator<T> queryIterator = container.GetItemQueryIterator<T>(
                queryDefinition,
                null,
            new QueryRequestOptions
            {
                    PartitionKey = new PartitionKey(partitionKey),
                    MaxConcurrency = -1,
                    MaxBufferedItemCount = maxItemCount,
                    MaxItemCount = maxItemCount
                });

            while (queryIterator.HasMoreResults)
            {
                FeedResponse<T> response = await queryIterator.ReadNextAsync();

                foreach (T item in response) { 
                    resources.Add(item);
                }
            }

            return resources;
        }
    }
}
