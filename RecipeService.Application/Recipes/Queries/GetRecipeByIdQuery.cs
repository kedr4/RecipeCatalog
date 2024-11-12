using MediatR;
using RecipeService.Domain.Recipes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RecipeService.Application.Recipes.Queries
{
    public class GetRecipeByIdQuery : IRequest<Recipe>
    {
        public int Id { get; set; }
    }
}
