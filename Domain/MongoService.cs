using Domain.AppSettings;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace Domain
{
    public interface IMongoService
    {
        public IMongoDatabase database { get; set; }
    };

    public class MongoService : IMongoService
    {
        public IMongoDatabase database { get; set; }

        public MongoService(IOptions<DatabaseConfiguration> databaseConfiguration)
        {
            var mongoClient = new MongoClient(
            databaseConfiguration.Value.ConnectionString);

            database = mongoClient.GetDatabase(
                databaseConfiguration.Value.Database);
        }
    }
}
