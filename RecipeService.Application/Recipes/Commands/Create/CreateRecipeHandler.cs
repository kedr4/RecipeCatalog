using MediatR;
using RecipeService.Domain.Recipes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RecipeService.Application.Recipes.Commands.Create
{
    internal class CreateRecipeHandler : IRequestHandler<CreateRecipeCommand, CreateRecipeResponse>
    {
        private readonly IRecipeRepository _repository;

        public CreateRecipeHandler(IRecipeRepository repository)
        {
            _repository = repository;
        }

        public async Task<CreateRecipeResponse> Handle(CreateRecipeCommand request, CancellationToken cancellationToken)
        {
            var id = await _repository.CreateRecipeAsync(request.Recipe);
            return new CreateRecipeResponse { Id = id };
        }
    }
}
