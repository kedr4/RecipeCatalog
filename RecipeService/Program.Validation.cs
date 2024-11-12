
using FluentValidation.AspNetCore;
using FluentValidation;
using RecipeService.Api.Controllers.Recipes.Requests;
using RecipeService.Api.Controllers.Recipes.Validators;
using RecipeService.Api.Controllers.Recipes.Requests.RecipeService.Api.Controllers.Recipes.Requests;

namespace RecipeService
{
    public static class ProgramValidation
    {
        public static IServiceCollection AddValidation(this IServiceCollection services)
        {
            services.AddScoped<IValidator<CreateRecipeRequest>, CreateRecipeRequestValidator>();
            services.AddScoped<IValidator<UpdateRecipeRequest>, UpdateRecipeRequestValidator>();
            services.AddFluentValidationAutoValidation();
            return services;
        }

    }
}
