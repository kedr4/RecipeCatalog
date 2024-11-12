using MediatR;
using RecipeService.Domain.Recipes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RecipeService.Application.Recipes.Queries
{
    public class GetRecipeByIdQueryHandler : IRequestHandler<GetRecipeByIdQuery, Recipe>
    {
        private readonly IRecipeRepository _repository;

        public GetRecipeByIdQueryHandler(IRecipeRepository repository)
        {
            _repository = repository;
        }

        public async Task<Recipe> Handle(GetRecipeByIdQuery request, CancellationToken cancellationToken)
        {
            return await _repository.GetRecipeByIdAsync(request.Id);
        }
    }
  
}
