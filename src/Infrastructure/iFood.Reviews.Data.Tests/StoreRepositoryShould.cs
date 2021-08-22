using iFood.Tests;
using MongoDB.Driver;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace iFood.Reviews.Data.Tests
{
    public class StoreRepositoryShould : IClassFixture<IntegrationFixture>
    {
        private readonly IntegrationFixture testFixture;

        public StoreRepositoryShould(IntegrationFixture testFixture)
        {
            this.testFixture = testFixture;
        }

        [ReviewsAutoData]
        [Theory]
        public async Task CreateStore_FromStoreName(string storeName)
        {
            //arrange
            var sut = new StoreRepository(testFixture.Context);

            //act
            var store = await sut.Add(storeName, CancellationToken.None);

            //assert
            var cursor = await testFixture.Context.Stores.FindAsync(Builders<Store>.Filter.Eq(x => x.Id, store.Id), cancellationToken: CancellationToken.None);
            var document = await cursor.FirstOrDefaultAsync();

            Assert.NotNull(document);
            Assert.Equal(storeName, store.Name);
            Assert.Equal(storeName, document.Name);
            Assert.Equal(0d, document.AverageRating);
            Assert.Empty(document.Reviews);
        }

        [ReviewsAutoData]
        [Theory]
        public async Task GetStore_FromId(IEnumerable<Store> documents)
        {
            //arrange
            var document = documents.FirstOrDefault();
            await testFixture.Context.Stores.InsertManyAsync(documents, cancellationToken: CancellationToken.None);
            var sut = new StoreRepository(testFixture.Context);

            //act
            var store = await sut.GetById(document.Id, CancellationToken.None);

            //assert
            Assert.Equal(document.Id, store.Id);
            Assert.Equal(document.Name, store.Name);
            Assert.Equal(document.AverageRating, store.AverageRating);
            Assert.Equal(document.Reviews.Count(), store.Reviews.Count());
        }

        [ReviewsAutoData]
        [Theory]
        public async Task SaveReviews(string storeName, IEnumerable<Review> reviews)
        {
            //arrange
            var sut = new StoreRepository(testFixture.Context);
            var store = await sut.Add(storeName, CancellationToken.None);
            store.AddReviews(reviews.ToArray());

            //act
            _ = await sut.SaveReviews(store, CancellationToken.None);

            //assert
            var cursor = await testFixture.Context.Stores
                .FindAsync(Builders<Store>.Filter.Eq(x => x.Id, store.Id), cancellationToken: CancellationToken.None);
            var document = await cursor.FirstOrDefaultAsync();

            Assert.Equal(document.Id, store.Id);
            Assert.Equal(document.Name, store.Name);
            Assert.Equal(document.AverageRating, store.AverageRating);
            Assert.Equal(document.Reviews.Count(), store.Reviews.Count());
        }
    }
}
