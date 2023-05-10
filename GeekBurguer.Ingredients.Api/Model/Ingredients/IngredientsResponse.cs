using GeekBurguer.Ingredients.Api.Model.Products;
using System.ComponentModel.DataAnnotations.Schema;

namespace GeekBurguer.Ingredients.Api.Model.Ingredients
{
    public class IngredientsResponse
    {

        public Guid ProductId { get; set; }

        public string[]? Ingredients { get; set; }

    }
}