using Basket.API.Models;
using Common.Extensions;
using System.Runtime.Caching;
using System.Text.Json;

namespace Basket.API.Data.Repository
{

    public class InMemoryCachedRepository(IBasketRepository repository, ObjectCache cache) : IBasketRepository
    {
        private readonly ObjectCache cache = cache;

        public Task<bool> DeleteBasket(string userName, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public async Task<ShoppingCart> GetBasket(string userName, CancellationToken cancellationToken = default)
        {
            ShoppingCart? cachedBasket = cache.Get(userName) as ShoppingCart;
            if (cachedBasket != null)
            {
                return cachedBasket;
            }

            ShoppingCart basket = await repository.GetBasket(
                userName,
                cancellationToken);

            cache.AddOrUpdateItemInCache(userName, basket);
            
            return basket;
        }

        public Task<ShoppingCart> StoreBasket(ShoppingCart basket, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }
    }
}
