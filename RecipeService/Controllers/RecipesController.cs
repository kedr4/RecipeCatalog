using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RecipeService.Api.Controllers.Recipes.Requests;
using RecipeService.Api.Controllers.Recipes.Requests.RecipeService.Api.Controllers.Recipes.Requests;
using RecipeService.Application.Recipes.Commands.Create;
using RecipeService.Application.Recipes.Queries;
using RecipeService.Domain.Recipes;
using System.Security.Claims;

namespace RecipeService.Api.Controllers
{
    [ApiController]
    [Authorize]
    [Route("api/recipes")]
    public class RecipesController : ControllerBase
    {
        private readonly IMediator _mediator;

        public RecipesController(IMediator mediator)
        {
            _mediator = mediator;
        }

        // POST: api/recipes
        [HttpPost]
        public async Task<ActionResult<Recipe>> CreateRecipeAsync([FromBody] CreateRecipeRequest request)
        {
            var userId = HttpContext.User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;
            var cmd = new CreateRecipeCommand
            {
                Recipe = new Recipe
                {
                    Title = request.Title,
                    Description = request.Description,
                    Category = request.Category,
                    Ingredients = request.Ingredients,
                    Instructions = request.Instructions,
                    CreatedAt = DateTime.UtcNow,
                    UserId = userId
                }
            };
            var id = await _mediator.Send(cmd);

            return Ok(id);
        }

        // GET: api/recipes
        [HttpGet]
        public async Task<ActionResult<List<Recipe>>> GetRecipesAsync()
        {
            var query = new GetRecipesQuery();
            var recipes = await _mediator.Send(query);
            return Ok(recipes);
        }

        // GET: api/recipes/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<Recipe>> GetRecipeByIdAsync(int id)
        {
            var query = new GetRecipeByIdQuery { Id = id };
            var recipe = await _mediator.Send(query);

            if (recipe == null)
                return NotFound();

            return Ok(recipe);
        }

        // PUT: api/recipes/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateRecipeAsync(int id, [FromBody] UpdateRecipeRequest request)
        {
            var cmd = new UpdateRecipeCommand
            {
                Recipe = new Recipe
                {
                    Id = id,
                    Title = request.Title,
                    Description = request.Description,
                    Category = request.Category,
                    Ingredients = request.Ingredients,
                    Instructions = request.Instructions,
                    //UpdatedAt = DateTime.UtcNow
                }
            };

            var response = await _mediator.Send(cmd);

            if (!response.IsSuccessful)
                return NotFound(response.Message);

            return NoContent();
        }

        // DELETE: api/recipes/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteRecipeAsync(int id)
        {
            var cmd = new DeleteRecipeCommand { RecipeId = id };
            var response = await _mediator.Send(cmd);

            if (!response.IsSuccessful)
                return NotFound(response.Message);

            return NoContent();
        }

        // GET: api/recipes/filter
        [HttpGet("filter")]
        public async Task<ActionResult<List<Recipe>>> GetRecipesByFilterAsync(
            [FromQuery] string title,
            [FromQuery] string category,
            [FromQuery] string ingredients,
            [FromQuery] DateTime? createdAfter)
        {
            var query = new GetRecipesQueryWithFilter
            {
                Title = title,
                Category = category,
                Ingredients = ingredients,
                CreatedAfter = createdAfter
            };

            var recipes = await _mediator.Send(query);
            return Ok(recipes);
        }
    }
}
