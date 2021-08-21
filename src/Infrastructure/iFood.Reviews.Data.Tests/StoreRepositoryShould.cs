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

        [MockableAutoData]
        [Theory]
        public async Task CreateStore_FromStoreName(string storeName)
        {
            //arrange
            var sut = new StoreRepository(testFixture.Context, testFixture.Mapper);

            //act
            var store = await sut.Add(storeName, CancellationToken.None);

            //assert
            var cursor = await testFixture.Context.Stores.FindAsync(Builders<StoreDb>.Filter.Eq(x => x.Id, store.Id), cancellationToken: CancellationToken.None);
            var dbStore = await cursor.FirstOrDefaultAsync();
            Assert.NotNull(dbStore);
            Assert.Equal(storeName, store.Name);
            Assert.Equal(storeName, dbStore.Name);
            Assert.Equal(0d, dbStore.AverageRating);
            Assert.Empty(dbStore.Reviews);
        }

        [MockableAutoData]
        [Theory]
        public async Task GetStore_FromId(IEnumerable<StoreDb> documents)
        {
            //arrange
            var document = documents.FirstOrDefault();
            await testFixture.Context.Stores.InsertManyAsync(documents, cancellationToken: CancellationToken.None);
            var sut = new StoreRepository(testFixture.Context, testFixture.Mapper);

            //act
            var store = await sut.GetById(document.Id, CancellationToken.None);

            //assert
            Assert.Equal(document.Id, store.Id);
            Assert.Equal(document.Name, store.Name);
            Assert.Equal(document.AverageRating, store.AverageRating);
            Assert.Equal(document.Reviews.Count(), store.Reviews.Count());
        }
    }
}
