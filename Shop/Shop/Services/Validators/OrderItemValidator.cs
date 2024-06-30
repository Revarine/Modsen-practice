using FluentValidation;
using Shop.Services.DTO;

namespace Shop.Services.Validators;

public class OrderItemDtoValidator : AbstractValidator<OrderItemDto>
{
    public OrderItemDtoValidator()
    {
        RuleFor(x => x.OrderId).GreaterThan(0).WithMessage("The OrderId must be greater than 0.");
        RuleFor(x => x.ProductId).GreaterThan(0).WithMessage("The ProductId must be greater than 0.");
        RuleFor(x => x.Quantity).GreaterThan(0).WithMessage("The Quantity must be greater than 0.");
    }
}