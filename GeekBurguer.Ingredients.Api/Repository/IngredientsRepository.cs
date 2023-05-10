using GeekBurguer.Ingredients.Api.Infra;
using GeekBurguer.Ingredients.Api.Model.Ingredients;
using Microsoft.EntityFrameworkCore;
using System.Collections;

public class IngredientsRepository : IIngredientsRepository
{
    private readonly IngredientsContext _context;

    public IngredientsRepository(IngredientsContext context)
    {
        _context = context;
    }

    public async Task<HashSet<IngredientsResponse>> GetProductsByRestrictions(IngredientsRequest ingredientsRequest)
    {
        var listaResponse = new HashSet<IngredientsResponse>();

        foreach (var restriction in ingredientsRequest.Restrictions)
        {
            var products = await _context.Items.Where(i => !string.IsNullOrEmpty(i.Ingredients) && !i.Ingredients.Contains(restriction)).ToListAsync();

            foreach(var product in products)
            {
                var productFound = _context.Products.Where(p => p.ProductId == product.ProductId && p.StoreName.ToLower() == ingredientsRequest.StoreName.ToLower()).FirstOrDefault() ;
                if(productFound is null)
                {
                    continue;
                }

                listaResponse.Add(new IngredientsResponse
                {
                    ProductId = productFound.ProductId,
                    Ingredients = product.Ingredients.Split(",")
                });
            }
        }


        return listaResponse;
    }
}