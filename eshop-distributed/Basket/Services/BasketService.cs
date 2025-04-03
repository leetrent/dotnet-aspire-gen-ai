using Microsoft.Extensions.Caching.Distributed;
using System.Text.Json;

namespace Basket.Services
{
    public class BasketService(IDistributedCache cache)
    {
        public async Task<ShoppingCart?> GetBasket(string userName)
        {
            var basket = await cache.GetStringAsync(userName);
            if (string.IsNullOrEmpty(basket))
            {
                return null;
            }
            return JsonSerializer.Deserialize<ShoppingCart>(basket);
        }

        public async Task UpdateBasket(ShoppingCart basket)
        {
            await cache.SetStringAsync(basket.UserName, JsonSerializer.Serialize(basket));
        }

        public async Task DeleteBasket(string userName)
        {
            await cache.RemoveAsync(userName);
        }
    }
}
