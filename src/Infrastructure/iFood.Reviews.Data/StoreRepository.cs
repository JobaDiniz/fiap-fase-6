using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace iFood.Reviews.Data
{
    public class StoreRepository : IStoreRepository
    {
        private readonly StoreContext context;

        public StoreRepository(StoreContext context)
        {
            if (context is null) throw new ArgumentNullException(nameof(context));

            this.context = context;
        }

        public async Task<Store> Add(string storeName, CancellationToken cancellation)
        {
            var store = new Store { Name = storeName };
            await context.Stores.InsertOneAsync(store, cancellationToken: cancellation);

            return store;
        }

        public async Task<Store> GetById(Guid id, CancellationToken cancellation)
        {
            var filter = Builders<Store>.Filter.Eq(x => x.Id, id);
            var cursor = await context.Stores.FindAsync(filter, cancellationToken: cancellation);
            var store = await cursor.FirstOrDefaultAsync(cancellation);
            return store;
        }

        public async Task<Store> SaveReviews(Store store, CancellationToken cancellation)
        {
            var reviews = store.Reviews.ToList();
            var filter = Builders<Store>.Filter.Eq(x => x.Id, store.Id);
            var update = Builders<Store>.Update
                .Set(x => x.AverageRating, store.AverageRating)
                .Set(new StringFieldDefinition<Store, IList<Review>>(StoreContextMongoConfiguration.StoreReviewsFieldName), reviews);

            return await context.Stores.FindOneAndUpdateAsync(filter, update, cancellationToken: cancellation);
        }
    }
}
