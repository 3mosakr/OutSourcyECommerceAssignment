using FluentValidation;
using OutSourcyECommerceAssignment.DTOs.OrderDTOs;

namespace OutSourcyECommerceAssignment.Validators.OrderValidators
{
    public class CreateOrderDtoValidator : AbstractValidator<CreateOrderDto>
    {
        public CreateOrderDtoValidator()
        {
            RuleFor(x => x.CustomerId)
                .GreaterThan(0).WithMessage("CustomerId is required.");

            RuleFor(x => x.OrderProducts)
                .NotEmpty().WithMessage("Order must contain at least one product.");

            RuleForEach(x => x.OrderProducts).ChildRules(orderItem =>
            {
                orderItem.RuleFor(i => i.ProductId)
                    .GreaterThan(0).WithMessage("ProductId must be greater than 0.");

                orderItem.RuleFor(i => i.Quantity)
                    .GreaterThan(0).WithMessage("Quantity must be greater than 0.");
            });
        }
    }
}
