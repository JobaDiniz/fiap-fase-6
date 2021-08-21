using AutoFixture.Xunit2;
using AutoMapper;
using MongoDB.Driver;
using Moq;
using System;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace iFood.Reviews.Data.Tests
{
    public class StoreRepositoryShould : IClassFixture<IntegrationFixture>
    {
        private readonly IntegrationFixture integrationFixture;

        public StoreRepositoryShould(IntegrationFixture integrationFixture)
        {
            this.integrationFixture = integrationFixture;
        }

        [MockableAutoData]
        [Theory]
        public async Task CreateStore_FromStoreName(string storeName, [Frozen] Mock<IMapper> mapper)
        {
            //https://dzone.com/articles/write-integration-tests-on-mongodb-with-net-core-a
            //https://github.com/Mongo2Go/Mongo2Go
            //arrange
            var id = Guid.Empty;
            mapper.Setup(m => m.Map<Store>(It.Is<StoreDb>(s => s.Name == storeName))).Callback<object>(c => id = ((StoreDb)c).Id);
            var sut = new StoreRepository(integrationFixture.Context, mapper.Object);

            //act
            _ = await sut.Add(storeName, CancellationToken.None);

            //assert
            var cursor = await integrationFixture.Context.Stores.FindAsync(Builders<StoreDb>.Filter.Eq(x => x.Id, id), cancellationToken: CancellationToken.None);
            var data = await cursor.FirstOrDefaultAsync();
            Assert.Equal(data.Name, storeName);
        }
    }
}
