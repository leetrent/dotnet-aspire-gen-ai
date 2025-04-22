using Catalog.Models;

namespace WebApp.ApiClients
{
    public class CatalogApiClient(HttpClient httpClient)
    {
        public async Task<List<Product>?> GetProducts()
        {
            return await httpClient.GetFromJsonAsync<List<Product>>($"/products");
        }

        public async Task<Product?> GetProductById(int id)
        {
            return await httpClient.GetFromJsonAsync<Product>($"/products/{id}");
        }

        public async Task<string?> SupportProducts(string query)
        {
            return await httpClient.GetFromJsonAsync<string>($"/products/support/{query}");
        }
    }
}
