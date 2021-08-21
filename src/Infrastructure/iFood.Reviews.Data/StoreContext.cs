using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.Conventions;
using MongoDB.Driver;
using System;
using System.Linq;

namespace iFood.Reviews.Data
{
    public sealed class StoreContext
    {
        private readonly IMongoClient client;
        private readonly IMongoDatabase database;

        static StoreContext()
        {
            BsonSerializer.RegisterSerializer(new RatingBsonSerializer());
            BsonClassMap.RegisterClassMap<Store>(c =>
            {
                c.AutoMap();
                c.MapIdProperty(m => m.Id);
                c.MapField("reviews").SetElementName(nameof(Store.Reviews));
            });

            BsonClassMap.RegisterClassMap<Review>(c =>
            {
                c.AutoMap();
                c.MapIdProperty(m => m.Id);
            });

            ConventionRegistry.Register(nameof(ImmutableTypeClassMapConvention),
                new ConventionPack { new ImmutableTypeClassMapConvention() }, type => new Type[] { typeof(Review) }.Contains(type));
        }

        public StoreContext(string connectionString, string databaseName)
        {
            if (connectionString is null) throw new ArgumentNullException(nameof(connectionString));
            if (databaseName is null) throw new ArgumentNullException(nameof(databaseName));

            client = new MongoClient(connectionString);
            database = client.GetDatabase(databaseName);
        }

        public StoreContext(StoreSettings settings)
            : this(settings.ConnectionString, settings.Database) { }

        public IMongoCollection<Store> Stores => database.GetCollection<Store>(nameof(Store));
    }

    public class StoreSettings
    {
        public StoreSettings() { }

        public StoreSettings(string connectionString, string database)
        {
            ConnectionString = connectionString;
            Database = database;
        }

        public string ConnectionString { get; set; }
        public string Database { get; set; }
    }
}
