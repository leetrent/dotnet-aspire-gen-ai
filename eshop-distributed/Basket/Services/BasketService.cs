using Catalog.Models;
using Microsoft.Extensions.Caching.Distributed;
using System.Text.Json;

namespace Basket.Services
{
    public class BasketService(IDistributedCache cache, CatalogApiClient catalogApiClient)
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

        public async Task UpdateBasket(ShoppingCart shoppingCart)
        {
            foreach (ShoppingCartItem scItem in shoppingCart.Items)
            {
                Product? product = await catalogApiClient.GetProductById(scItem.ProductId);
                if (product != null) // Ensure product is not null before accessing its properties
                {
                    scItem.Price = product.Price;
                    scItem.ProductName = product.Name;
                }
            }
            await cache.SetStringAsync(shoppingCart.UserName, JsonSerializer.Serialize(shoppingCart));
        }

        public async Task DeleteBasket(string userName)
        {
            await cache.RemoveAsync(userName);
        }
    }
}
