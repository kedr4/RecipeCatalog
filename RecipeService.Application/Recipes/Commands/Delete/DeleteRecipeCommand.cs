using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RecipeService.Application.Recipes.Commands.Create
{
    public class DeleteRecipeCommand : IRequest<DeleteRecipeResponse>
    {
        public int RecipeId { get; set; }
    }
}
