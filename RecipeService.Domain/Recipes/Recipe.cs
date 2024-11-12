using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RecipeService.Domain.Recipes
{
    public class Recipe
    {
        public int Id { get; set; }
        public string Title { get; set; }          // Название рецепта
        public string Description { get; set; }    // Описание рецепта
        public string Category { get; set; }       // Категория рецепта
        public string Ingredients { get; set; }    // Ингредиенты (можно сделать отдельной сущностью)
        public string Instructions { get; set; }   // Шаги приготовления
        public DateTime CreatedAt { get; set; }    // Дата создания рецепта
        //public string CreatedBy { get; set; }      // Автор рецепта (ID пользователя)
        public string UserId { get; set; }
        // Дополнительные свойства, например, для изображения
        public string? ImageUrl { get; set; }       // Ссылка на изображение
    }
}
