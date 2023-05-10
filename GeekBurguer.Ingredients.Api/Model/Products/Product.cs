using GeekBurguer.Ingredients.Api.Model.Products.Response;
using System.ComponentModel.DataAnnotations;

namespace GeekBurguer.Ingredients.Api.Model.Products
{
    public class Product
    {
        [Key]
        public Guid ProductId { get; set; } = Guid.NewGuid();
        public string StoreName { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;
        public string Image { get; set; } = string.Empty;
        public ICollection<Item> Items { get; set; } = new List<Item>();
        public double Price { get; set; }
    }
}
