using RecipeService.Domain.Recipes;
using RecipeService.Infrastructure.Database.Repositories;
namespace RecipeService
{
    public static class ProgramServices
    {
        public static IServiceCollection AddServices(this IServiceCollection services)
        {
            services.AddMediator();
            services.AddScoped<IRecipeRepository, RecipeRepository>();


            return services;
        }



    }
}
