using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RecipeService.Application.Recipes.Commands.Create
{
    public class UpdateRecipeResponse
    {
        public bool IsSuccessful { get; set; }
        public string Message { get; set; }
    }
}
