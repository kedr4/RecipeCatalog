using MediatR;
using RecipeService.Domain.Recipes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RecipeService.Application.Recipes.Commands.Create
{
    internal class UpdateRecipeHandler : IRequestHandler<UpdateRecipeCommand, UpdateRecipeResponse>
    {
        private readonly IRecipeRepository _repository;

        public UpdateRecipeHandler(IRecipeRepository repository)
        {
            _repository = repository;
        }

        public async Task<UpdateRecipeResponse> Handle(UpdateRecipeCommand request, CancellationToken cancellationToken)
        {
            var existingRecipe = await _repository.GetRecipeByIdAsync(request.Recipe.Id);
            if (existingRecipe == null)
            {
                return new UpdateRecipeResponse { IsSuccessful = false, Message = "Recipe not found." };
            }

            existingRecipe.Title = request.Recipe.Title;
            existingRecipe.Description = request.Recipe.Description;
            existingRecipe.Category = request.Recipe.Category;
            existingRecipe.Ingredients = request.Recipe.Ingredients;
            existingRecipe.Instructions = request.Recipe.Instructions;

            // Обновляем рецепт и получаем идентификатор
            var updatedRecipeId = await _repository.UpdateRecipeAsync(existingRecipe);

            return new UpdateRecipeResponse
            {
                IsSuccessful = updatedRecipeId > 0, // Успех, если идентификатор больше 0
                Message = updatedRecipeId > 0 ? "Recipe updated successfully." : "Failed to update recipe."
            };
        }
    }
}
