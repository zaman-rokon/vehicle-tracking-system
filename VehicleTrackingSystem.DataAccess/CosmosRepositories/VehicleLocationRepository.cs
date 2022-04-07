using Microsoft.Azure.Cosmos;
using Microsoft.Extensions.Options;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using VehicleTrackingSystem.DataAccess.DBModels;

using PartitionKey = Microsoft.Azure.Cosmos.PartitionKey;

namespace VehicleTrackingSystem.DataAccess.CosmosRepositories
{
    public class VehicleLocationRepository : QueryRepository<VehicleLocation>, IVehicleLocationRepository
    {
        public VehicleLocationRepository(CosmosClient client, IOptions<QueryDbSettings> options)
           : base(client, options.Value)
        {

        }

        public async Task<IEnumerable<VehicleLocation>> GetAllAsync(string vehicleId, DateTime startDate, DateTime endDate)
        {
            var partitionKey = GetPartitionKey(vehicleId, startDate);
            IQueryable<QueryDocument<VehicleLocation>> queryable = Container.GetItemLinqQueryable<QueryDocument<VehicleLocation>>(
                      requestOptions: new QueryRequestOptions
                      {
                          PartitionKey = new PartitionKey(partitionKey),
                      }).Where(x => x.Data.TrackDateTime >= startDate.ToUniversalTime() && x.Data.TrackDateTime <= endDate.ToUniversalTime());

            var vehicleLocation = await GetListFromFeedIterator(queryable);
            return vehicleLocation;
        }

        public async Task<VehicleLocation> GetLastLocationAsync(string vehicleId, DateTime startDate)
        {
            var partitionKey = GetPartitionKey(vehicleId, startDate);
            IQueryable<QueryDocument<VehicleLocation>> queryable = Container
                .GetItemLinqQueryable<QueryDocument<VehicleLocation>>(
                    requestOptions: new QueryRequestOptions
                    {
                        PartitionKey = new PartitionKey(partitionKey),
                    }).OrderBy(x => x.Data.TrackDateTime);

            var vehicleLocation = await GetListFromFeedIterator(queryable);
            return vehicleLocation.LastOrDefault();
        }

        private string GetPartitionKey(string vehicleId, DateTime startDate)
        {
            return $"{vehicleId}|Location|{startDate.ToUniversalTime():dd-MM-yyyy}";
        }

    }
}
