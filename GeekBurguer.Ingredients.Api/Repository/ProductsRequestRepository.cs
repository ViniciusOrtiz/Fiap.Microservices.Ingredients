using GeekBurguer.Ingredients.Api.Infra;
using GeekBurguer.Ingredients.Api.Model.Products;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

namespace GeekBurguer.Ingredients.Api.Repository
{
    public class ProductsRequestRepository : IProductsRequestRepository
    {
        private readonly ApiClient _apiClient;
        private readonly IngredientsContext _context;
        private readonly IConfiguration _configuration;

        public ProductsRequestRepository(ApiClient apiClient, IngredientsContext context, IConfiguration configuration)
        {
            _apiClient = apiClient;
            _context = context;
            _configuration = configuration;
        }
        public async Task GetProducts()
        {
            var items = await _context.Items.ToListAsync();
            _context.Items.RemoveRange(items);

            var products = await _context.Products.ToListAsync();
            _context.Products.RemoveRange(products);
            await _context.SaveChangesAsync();
            var stores = _configuration.GetSection("Services:Products:storeNames").Get<string[]>();
            foreach (var store in stores)
            {
                var response = await _apiClient.GetProducts(store);
                //// Lê o arquivo JSON
                //string json = File.ReadAllText("products.json");

                //// Converte o JSON em um objeto C#
                //var response = JsonConvert.DeserializeObject<List<Product>>(json);
                foreach (var product in response)
                {
                    product.ProductId = Guid.NewGuid();
                    product.StoreName = store;

                    foreach (var item in product.Items)
                    {
                        var itemFound = _context.Items.FirstOrDefault(i => i.Name == product.Name);

                        item.ItemId = Guid.NewGuid();
                        if (itemFound is not null)
                        {
                            item.ItemId = itemFound.ItemId;
                        }
                    }
                    
                    _context.Products.Add(product);
                }
            }

            await _context.SaveChangesAsync();
        }
    }
}