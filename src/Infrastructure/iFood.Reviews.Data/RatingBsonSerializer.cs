using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.Serializers;

namespace iFood.Reviews.Data
{
    class RatingBsonSerializer : StructSerializerBase<Rating>, IRepresentationConfigurable<RatingBsonSerializer>
    {
        public RatingBsonSerializer()
            : this(BsonType.Double) { }

        public RatingBsonSerializer(BsonType bsonType) => Representation = bsonType;

        public BsonType Representation { get; }

        public RatingBsonSerializer WithRepresentation(BsonType representation) => new RatingBsonSerializer(representation);
        IBsonSerializer IRepresentationConfigurable.WithRepresentation(BsonType representation) => WithRepresentation(representation);

        public override Rating Deserialize(BsonDeserializationContext context, BsonDeserializationArgs args)
        {
            var bsonReader = context.Reader;
            var bsonType = bsonReader.GetCurrentBsonType();

            return bsonType switch
            {
                BsonType.Double => new Rating(bsonReader.ReadDouble()),
                _ => throw CreateCannotDeserializeFromBsonTypeException(bsonType),
            };
        }

        public override void Serialize(BsonSerializationContext context, BsonSerializationArgs args, Rating value)
        {
            var bsonWriter = context.Writer;
            switch (Representation)
            {
                case BsonType.Double: bsonWriter.WriteDouble(value); break;
                default: throw new BsonSerializationException($"'{Representation}' is not a valid {nameof(Rating)} representation.");
            }
        }
    }
}
