namespace GeekBurguer.Ingredients.Api.Model.Products.Response
{
    public class ProductsResponse
    {
        public int StatusCode { get; set; }
        public string ErrorMessage { get; set; }
        public List<Product> Products { get; set; }
    }
}