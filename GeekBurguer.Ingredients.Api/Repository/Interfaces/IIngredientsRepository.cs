using GeekBurguer.Ingredients.Api.Model.Ingredients;

public interface IIngredientsRepository
{

    Task<HashSet<IngredientsResponse>> GetProductsByRestrictions(IngredientsRequest ingredientsRequest);
}