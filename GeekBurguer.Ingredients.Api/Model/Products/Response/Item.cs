using System.ComponentModel.DataAnnotations;

namespace GeekBurguer.Ingredients.Api.Model.Products.Response
{
    public class Item
    {
        [Key]
        public Guid ItemId { get; set; }
        public string Name { get; set; } = string.Empty;    
        public string Ingredients { get; set; } = string.Empty;
        public Guid ProductId { get; set; }
    }
}