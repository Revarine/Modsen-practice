using FluentValidation;
using Shop.Services.DTO;

namespace Shop.Services.Validators
{
    public class CategoryDtoValidator : AbstractValidator<CategoryDto>
    {
        public CategoryDtoValidator()
        {
            RuleFor(x => x.Name).NotEmpty().WithMessage("Category name cannot be empty.");
        }
    }
}
