using FluentValidation;
using RecipeService.Api.Controllers.Recipes.Requests;
using RecipeService.Api.Controllers.Recipes.Requests.RecipeService.Api.Controllers.Recipes.Requests;

namespace RecipeService.Api.Controllers.Recipes.Validators
{
    public class UpdateRecipeRequestValidator : AbstractValidator<UpdateRecipeRequest>
    {
        public UpdateRecipeRequestValidator()
        {
            RuleFor(r => r.Title)
                .NotEmpty().WithMessage("Название не может быть пустым.")
                .When(r => !string.IsNullOrEmpty(r.Title))
                .MaximumLength(100).WithMessage("Название не должно превышать 100 символов.");

            RuleFor(r => r.Description)
                .NotEmpty().WithMessage("Описание не может быть пустым.")
                .When(r => !string.IsNullOrEmpty(r.Description))
                .MaximumLength(500).WithMessage("Описание не должно превышать 500 символов.");

            RuleFor(r => r.Category)
                .NotEmpty().WithMessage("Категория не может быть пустой.")
                .When(r => !string.IsNullOrEmpty(r.Category));

            RuleFor(r => r.Ingredients)
                .NotEmpty().WithMessage("Ингредиенты не могут быть пустыми.")
                .When(r => !string.IsNullOrEmpty(r.Ingredients));

            RuleFor(r => r.Instructions)
                .NotEmpty().WithMessage("Инструкции не могут быть пустыми.")
                .When(r => !string.IsNullOrEmpty(r.Instructions));

            //RuleFor(r => r.ImageUrl)
            //    .Must(uri => Uri.TryCreate(uri, UriKind.Absolute, out _))
            //    .When(r => !string.IsNullOrEmpty(r.ImageUrl))
            //    .WithMessage("Некорректный формат URL для изображения.");
        }
    }
}
