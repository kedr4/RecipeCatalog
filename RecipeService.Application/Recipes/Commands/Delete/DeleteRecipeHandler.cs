using MediatR;
using RecipeService.Domain.Recipes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RecipeService.Application.Recipes.Commands.Create
{
    internal class DeleteRecipeHandler : IRequestHandler<DeleteRecipeCommand, DeleteRecipeResponse>
    {
        private readonly IRecipeRepository _repository;

        public DeleteRecipeHandler(IRecipeRepository repository)
        {
            _repository = repository;
        }

        public async Task<DeleteRecipeResponse> Handle(DeleteRecipeCommand request, CancellationToken cancellationToken)
        {
            var recipe = await _repository.GetRecipeByIdAsync(request.RecipeId);
            if (recipe == null)
            {
                return new DeleteRecipeResponse { IsSuccessful = false, Message = "Recipe not found." };
            }

            var result = await _repository.DeleteRecipeAsync(recipe);
            return new DeleteRecipeResponse { IsSuccessful = result, Message = result ? "Recipe deleted successfully." : "Failed to delete recipe." };
        }
    }
}
