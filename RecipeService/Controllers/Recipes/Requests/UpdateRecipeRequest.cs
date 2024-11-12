namespace RecipeService.Api.Controllers.Recipes.Requests
{
    namespace RecipeService.Api.Controllers.Recipes.Requests
    {
        public class UpdateRecipeRequest
        {
            public string Title { get; set; }           // Название рецепта
            public string Description { get; set; }     // Краткое описание рецепта
            public string Category { get; set; }        // Категория рецепта (например, "Супы", "Закуски")
            public string Ingredients { get; set; }     // Ингредиенты рецепта
            public string Instructions { get; set; }    // Шаги приготовления
            public string ImageUrl { get; set; }        // Ссылка на изображение рецепта (опционально)
        }
    }

}
