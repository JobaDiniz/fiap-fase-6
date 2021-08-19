using System;
using System.Threading;
using System.Threading.Tasks;

namespace iFood.Reviews
{
    public interface IStoreRepository
    {
        Task<Store> Add(string storeName, CancellationToken cancellation);
        Task SaveReviews(Store store, CancellationToken cancellation);
        Task<Store> GetById(Guid id, CancellationToken cancellation);
    }
}
