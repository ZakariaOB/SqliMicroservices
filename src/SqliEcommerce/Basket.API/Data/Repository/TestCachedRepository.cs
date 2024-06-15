using Basket.API.Models;
using Microsoft.Extensions.Caching.Distributed;

namespace Basket.API.Data.Repository
{
    public class TestCachedRepository(IBasketRepository repository, IDistributedCache cache) 
        : IBasketRepository
    {

        public Task<bool> DeleteBasket(string userName, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public async Task<ShoppingCart> GetBasket(string userName, CancellationToken cancellationToken = default)
        {
            return await Task.FromResult(new ShoppingCart
            {
                UserName = "TEST CACHE",
            });
        }

        public Task<ShoppingCart> StoreBasket(ShoppingCart basket, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }
    }
}
