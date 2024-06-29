using FluentValidation;
using Shop.Services.DTO;

namespace Shop.Services.Validators
{
    public class ProductDtoValidator : AbstractValidator<ProductDto>
    {
        public ProductDtoValidator()
        {
            RuleFor(x => x.Name).NotEmpty().WithMessage("The product name cannot be empty.");
            RuleFor(x => x.Price).GreaterThan(0).WithMessage("The price of the product must be greater than 0.");
            RuleFor(x => x.CategoryId).GreaterThan(0).WithMessage("The product category must be selected.");
        }
    }
}
