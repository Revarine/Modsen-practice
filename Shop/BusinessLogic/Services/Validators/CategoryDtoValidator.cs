using BusinessLogic.Services.DTO;
using FluentValidation;

namespace BusinessLogic.Services.Validators
{
    public class CategoryDtoValidator : AbstractValidator<CategoryDto>
    {
        public CategoryDtoValidator()
        {
            RuleFor(x => x.Name).NotEmpty().WithMessage("Category name cannot be empty.");
        }
    }
}
