using AutoMapper;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace iFood.Reviews.Data
{
    public class StoreRepository : IStoreRepository
    {
        private readonly StoreContext context;
        private readonly IMapper mapper;

        public StoreRepository(StoreContext context, IMapper mapper)
        {
            if (context is null) throw new ArgumentNullException(nameof(context));
            if (mapper is null) throw new ArgumentNullException(nameof(mapper));

            this.context = context;
            this.mapper = mapper;
        }

        public async Task<Store> Add(string storeName, CancellationToken cancellation)
        {
            var db = new StoreDb { Name = storeName };
            await context.Stores.InsertOneAsync(db, cancellationToken: cancellation);

            return mapper.Map<Store>(db);
        }

        public async Task<Store> GetById(Guid id, CancellationToken cancellation)
        {
            var filter = Builders<StoreDb>.Filter.Eq(x => x.Id, id);
            var cursor = await context.Stores.FindAsync(filter, cancellationToken: cancellation);
            var db = await cursor.FirstOrDefaultAsync(cancellation);
            var store = mapper.Map<Store>(db);
            return store;
        }

        public async Task SaveReviews(Store store, CancellationToken cancellation)
        {
            var filter = Builders<StoreDb>.Filter.Eq(x => x.Id, store.Id);
            var update = Builders<StoreDb>.Update
                .Set(x => x.AverageRating, store.AverageRating)
                .Set(x => x.Reviews, mapper.Map<IEnumerable<ReviewDb>>(store.Reviews));

            await context.Stores.FindOneAndUpdateAsync(filter, update, cancellationToken: cancellation);
        }
    }
}
