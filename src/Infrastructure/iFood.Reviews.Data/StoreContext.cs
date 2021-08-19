using MongoDB.Driver;
using System;

namespace iFood.Reviews.Data
{
    public sealed class StoreContext
    {
        private readonly IMongoClient client;
        private readonly IMongoDatabase database;

        public StoreContext(string connectionString, string databaseName)
        {
            if (connectionString is null) throw new ArgumentNullException(nameof(connectionString));
            if (databaseName is null) throw new ArgumentNullException(nameof(databaseName));

            client = new MongoClient(connectionString);
            database = client.GetDatabase(databaseName);
        }

        public StoreContext(StoreSettings settings)
            : this(settings.ConnectionString, settings.Database) { }

        public IMongoCollection<StoreDb> Stores => database.GetCollection<StoreDb>(StoreDb.CollectionName);
    }

    public class StoreSettings
    {
        public string ConnectionString { get; set; }
        public string Database { get; set; }
    }
}
