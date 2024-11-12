using FluentValidation;
using RecipeService.Api.Controllers.Recipes.Requests;

namespace RecipeService.Api.Controllers.Recipes.Validators
{
    public class CreateRecipeRequestValidator : AbstractValidator<CreateRecipeRequest>
    {
        public CreateRecipeRequestValidator()
        {
            RuleFor(r => r.Title)
                .NotEmpty().WithMessage("Название не может быть пустым.")
                .MaximumLength(100).WithMessage("Название не должно превышать 100 символов.");

            RuleFor(r => r.Description)
                .NotEmpty().WithMessage("Описание не может быть пустым.")
                .MaximumLength(500).WithMessage("Описание не должно превышать 500 символов.");

            RuleFor(r => r.Category)
                .NotEmpty().WithMessage("Категория не может быть пустой.");

            RuleFor(r => r.Ingredients)
                .NotEmpty().WithMessage("Ингредиенты не могут быть пустыми.");

            RuleFor(r => r.Instructions)
                .NotEmpty().WithMessage("Инструкции не могут быть пустыми.");

            RuleFor(r => r.ImageUrl)
                .Must(uri => Uri.TryCreate(uri, UriKind.Absolute, out _))
                .When(r => !string.IsNullOrEmpty(r.ImageUrl))
                .WithMessage("Некорректный формат URL для изображения.");
        }
    }
}
