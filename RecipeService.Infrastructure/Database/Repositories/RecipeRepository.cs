using Microsoft.EntityFrameworkCore;
using RecipeService.Domain.Recipes;


namespace RecipeService.Infrastructure.Database.Repositories
{
    public class RecipeRepository : IRecipeRepository
    {
        private readonly DatabaseContext _context;

        public RecipeRepository(DatabaseContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public async Task<int> CreateRecipeAsync(Recipe recipe)
        {
            if (recipe == null) throw new ArgumentNullException(nameof(recipe));

            await _context.Recipes.AddAsync(recipe);
            await _context.SaveChangesAsync();
            return recipe.Id; // Assuming `Id` is the primary key
        }

        public async Task<bool> DeleteRecipeAsync(Recipe recipe)
        {
            if (recipe == null) throw new ArgumentNullException(nameof(recipe));

            _context.Recipes.Remove(recipe);
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<Recipe> GetRecipeByIdAsync(int id)
        {
            return await _context.Recipes
                .AsNoTracking()
                .FirstOrDefaultAsync(r => r.Id == id);
        }
        public async Task<List<Recipe>> GetRecipesByUserIdAsync(string userId)
        {
            return await _context.Recipes
                .AsNoTracking()
                .Where(r => r.UserId == userId)
                .ToListAsync();
        }
        public async Task<List<Recipe>> GetAllRecipesAsync()
        {
            return await _context.Recipes
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task<int> UpdateRecipeAsync(Recipe recipe)
        {
            if (recipe == null) throw new ArgumentNullException(nameof(recipe));

            _context.Recipes.Update(recipe);
            await _context.SaveChangesAsync();
            return recipe.Id;
        }

        public async Task<List<Recipe>> SearchRecipesByTitleAsync(string title)
        {
            return await _context.Recipes
                .AsNoTracking()
                .Where(r => r.Title.Contains(title))
                .ToListAsync();
        }

        public async Task<List<Recipe>> GetRecipesByCategoryAsync(string category)
        {
            return await _context.Recipes
                .AsNoTracking()
                .Where(r => r.Category == category)
                .ToListAsync();
        }
    }
}
