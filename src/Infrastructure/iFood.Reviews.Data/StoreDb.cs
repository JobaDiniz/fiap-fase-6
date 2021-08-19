using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;

namespace iFood.Reviews.Data
{
    public class StoreDb
    {
        public readonly static string CollectionName = "StoreCollection";

        [BsonId]
        public Guid Id { get; set; }
        public string Name { get; set; }
        public double AverageRating { get; set; }
        public IEnumerable<ReviewDb> Reviews { get; set; }
    }
}
