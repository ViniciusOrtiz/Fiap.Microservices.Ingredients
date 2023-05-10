using GeekBurguer.Ingredients.Api.Model.Ingredients;
using Microsoft.AspNetCore.Mvc;

namespace GeekBurguer.Ingredients.Api.Controllers
{
    [ApiController]
    [Route("api/products")]
    public class IngredientsController : ControllerBase
    {
        private readonly IIngredientsRepository _ingredientsRepository;

        public IngredientsController(IIngredientsRepository ingredientsRepository)
        {
            _ingredientsRepository = ingredientsRepository;
        }

        [HttpPost("byrestrictions")]
        public async Task<ActionResult<IEnumerable<IngredientsResponse>>> GetProductsByRestrictions(IngredientsRequest ingredientsRequest)
        {
            try
            {
                var restrictedProducts = await _ingredientsRepository.GetProductsByRestrictions(ingredientsRequest);

                return Ok(restrictedProducts);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }
    }
}
