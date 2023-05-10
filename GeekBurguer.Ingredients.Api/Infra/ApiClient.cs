using Flurl;
using Flurl.Http;
using GeekBurguer.Ingredients.Api.Model.Products;

namespace GeekBurguer.Ingredients.Api.Infra;

public class ApiClient
{
    private readonly string _url;
    private const string ProductsEndpoint = "api/products";

    public ApiClient(IConfiguration configuration)
    {
        _url = configuration.GetValue<string>("Services:Products:ApiBaseAddress");
    }

    public async Task<List<Product>> GetProducts(string storeName)
    {
        return await _url.AppendPathSegment(ProductsEndpoint).SetQueryParam("storeName", storeName).GetJsonAsync<List<Product>>();
    }
}