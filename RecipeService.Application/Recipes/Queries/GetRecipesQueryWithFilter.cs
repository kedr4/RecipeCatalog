using MediatR;
using RecipeService.Domain.Recipes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RecipeService.Application.Recipes.Queries
{
    public class GetRecipesQueryWithFilter : IRequest<List<Recipe>>
    {
        public string Title { get; set; }
        public string Category { get; set; }
        public string Ingredients { get; set; }
        public string CreatedBy { get; set; }
        public DateTime? CreatedAfter { get; set; }
        public DateTime? CreatedBefore { get; set; }
    }
}
