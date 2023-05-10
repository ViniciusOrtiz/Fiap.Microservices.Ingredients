namespace GeekBurguer.Ingredients.Api.Model.Products
{
    public class ProductsRequest
    {
        public string StoreName { get; set; }
        public List<Product> Products { get; set; }
    }
}
