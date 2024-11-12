using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RecipeService.Domain.Recipes
{
    public interface IRecipeRepository
    {
        // Добавить новый рецепт
        Task<int> CreateRecipeAsync(Recipe recipe);

        // Получить рецепт по ID
        Task<Recipe> GetRecipeByIdAsync(int id);

        // Получить все рецепты
        Task<List<Recipe>> GetAllRecipesAsync();

        // Обновить существующий рецепт
        Task<int> UpdateRecipeAsync(Recipe recipe);

        // Удалить рецепт
        Task<bool> DeleteRecipeAsync(Recipe recipe);
        Task<List<Recipe>> GetRecipesByUserIdAsync(string userId);


        Task<List<Recipe>> SearchRecipesByTitleAsync(string title);    // Поиск по названию

        Task<List<Recipe>> GetRecipesByCategoryAsync(string category); // Фильтрация по категории
    }
}
