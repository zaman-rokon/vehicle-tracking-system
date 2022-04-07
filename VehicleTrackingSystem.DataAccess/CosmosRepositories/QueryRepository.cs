using Microsoft.Azure.Cosmos.Linq;
using Microsoft.Azure.Cosmos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace VehicleTrackingSystem.DataAccess.CosmosRepositories
{
    public abstract class QueryRepository<T>
    {
        protected Container Container { get; }

        protected QueryRepository(CosmosClient client, QueryDbSettings options)
        {
            if (client == null)
            {
                throw new ArgumentNullException(nameof(client));
            }

            if (options == null)
            {
                throw new ArgumentNullException(nameof(options));
            }
            Container = GetContainer(client, options);
        }

        private static Container GetContainer(CosmosClient client, QueryDbSettings options)
        {
            return client.GetContainer(options.DatabaseName, options.ContainerName);
        }

        protected static async Task<List<T>> GetListFromFeedIterator(IQueryable<QueryDocument<T>> queryable)
        {
            var feedIterator = queryable.ToFeedIterator();
            var result = new List<T>();
            while (feedIterator.HasMoreResults)
            {
                var results = await feedIterator.ReadNextAsync();
                result.AddRange(results.Select(res => res.Data));
            }

            return result;
        }

    }
}
