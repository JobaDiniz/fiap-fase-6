using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.Conventions;
using System;
using System.Linq;

namespace iFood.Reviews.Data
{
    internal class StoreContextMongoConfiguration
    {
        public static readonly string StoreReviewsFieldName = "reviews";

        internal static void Configure()
        {
            ConfigureSerializers();
            ConfigureStore();
            ConfigureReview();
            ConfigureConventions();
        }

        private static void ConfigureSerializers() => BsonSerializer.RegisterSerializer(new RatingBsonSerializer());

        private static void ConfigureConventions() => ConventionRegistry.Register(nameof(ImmutableTypeClassMapConvention),
                        new ConventionPack { new ImmutableTypeClassMapConvention() }, type => new Type[] { typeof(Review) }.Contains(type));

        private static void ConfigureReview() => BsonClassMap.RegisterClassMap<Review>(c =>
        {
            c.AutoMap();
            c.MapIdProperty(m => m.Id);
        });

        private static void ConfigureStore() => BsonClassMap.RegisterClassMap<Store>(c =>
        {
            c.AutoMap();
            c.MapIdProperty(m => m.Id);
            c.MapField(StoreReviewsFieldName).SetElementName(nameof(Store.Reviews));
        });
    }
}