using MediatR;
using RecipeService.Domain.Recipes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RecipeService.Application.Recipes.Queries
{
    public class GetRecipesQueryHandler : IRequestHandler<GetRecipesQuery, List<Recipe>>
    {
        private readonly IRecipeRepository _repository;

        public GetRecipesQueryHandler(IRecipeRepository repository)
        {
            _repository = repository;
        }

        public async Task<List<Recipe>> Handle(GetRecipesQuery request, CancellationToken cancellationToken)
        {
            return await _repository.GetAllRecipesAsync();
        }
    }
}
