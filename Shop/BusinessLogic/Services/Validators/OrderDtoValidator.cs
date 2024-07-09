using BusinessLogic.Services.DTO;
using FluentValidation;

namespace BusinessLogic.Services.Validators
{
    public class OrderDtoValidator : AbstractValidator<OrderDto>
    {
        public OrderDtoValidator()
        {
            RuleFor(x => x.UserId).GreaterThan(0).WithMessage("A valid user must be associated with the order.");
        }
    }
}
