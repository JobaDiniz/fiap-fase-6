using System;
using System.Threading;
using System.Threading.Tasks;

namespace iFood.Reviews
{
    public interface IStoreRepository
    {
        Task<Store> Add(Guid storeId, string storeName, CancellationToken cancellation);
        Task<Store> SaveReviews(Store store, CancellationToken cancellation);
        Task<Store> GetById(Guid id, CancellationToken cancellation);
    }
}
