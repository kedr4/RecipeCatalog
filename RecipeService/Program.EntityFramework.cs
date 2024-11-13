
using Microsoft.EntityFrameworkCore;
using RecipeService.Infrastructure.Database;

namespace RecipeService
{
    public static class ProgramEntityFramework
    {
        public static IServiceCollection AddEntityFramework(this IServiceCollection services, IConfiguration configuration)
        {
            //services.AddDbContext<DatabaseContext>(options => options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));
            //services.AddDbContext<DatabaseContext>(options =>
            //   options.UseInMemoryDatabase("RecipeCatalogDb2"));

            services.AddDbContext<DatabaseContext>(options =>
        options.UseSqlServer(configuration.GetConnectionString("DefaultConnection"),
            b => b.MigrationsAssembly("RecipeService.Infrastructure")));
            return services;
        }
    }
}
