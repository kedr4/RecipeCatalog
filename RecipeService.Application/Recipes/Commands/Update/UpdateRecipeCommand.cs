using MediatR;
using RecipeService.Domain.Recipes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RecipeService.Application.Recipes.Commands.Create
{
    public class UpdateRecipeCommand : IRequest<UpdateRecipeResponse>
    {
        public Recipe Recipe { get; set; }
    }
}
