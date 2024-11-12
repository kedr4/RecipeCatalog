using MediatR;
using RecipeService.Domain.Recipes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RecipeService.Application.Recipes.Queries
{
    public class GetRecipesQueryWithFilterHandler : IRequestHandler<GetRecipesQueryWithFilter, List<Recipe>>
    {
        private readonly IRecipeRepository _repository;

        public GetRecipesQueryWithFilterHandler(IRecipeRepository repository)
        {
            _repository = repository;
        }

        public async Task<List<Recipe>> Handle(GetRecipesQueryWithFilter request, CancellationToken cancellationToken)
        {
            var recipes = await _repository.GetAllRecipesAsync();

            if (!string.IsNullOrEmpty(request.Title))
            {
                recipes = recipes.Where(r => r.Title.Contains(request.Title)).ToList();
            }

            if (!string.IsNullOrEmpty(request.Category))
            {
                recipes = recipes.Where(r => r.Category == request.Category).ToList();
            }

            if (!string.IsNullOrEmpty(request.Ingredients))
            {
                recipes = recipes.Where(r => r.Ingredients.Contains(request.Ingredients)).ToList();
            }

            if (!string.IsNullOrEmpty(request.CreatedBy))
            {
                recipes = recipes.Where(r => r.CreatedBy == request.CreatedBy).ToList();
            }

            if (request.CreatedAfter.HasValue)
            {
                recipes = recipes.Where(r => r.CreatedAt >= request.CreatedAfter.Value).ToList();
            }

            if (request.CreatedBefore.HasValue)
            {
                recipes = recipes.Where(r => r.CreatedAt <= request.CreatedBefore.Value).ToList();
            }

            return recipes;
        }
    }
}
