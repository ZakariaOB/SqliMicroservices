using Basket.API.Data;
using Basket.API.Models;
using Common.CQRS;
using Discount.Grpc;
using FluentValidation;

namespace Basket.API.Basket.StoreBasket;

public record StoreBasketCommand(ShoppingCart Cart) : ICommand<StoreBasketResult>;
public record StoreBasketResult(string UserName);

public class StoreBasketCommandValidator : AbstractValidator<StoreBasketCommand>
{
    public StoreBasketCommandValidator()
    {
        RuleFor(x => x.Cart).NotNull().WithMessage("Cart can not be null");
        RuleFor(x => x.Cart.UserName).NotEmpty().WithMessage("UserName is required");
    }
}

public class StoreBasketCommandHandler
    (IBasketRepository repository, DiscountProtoService.DiscountProtoServiceClient discountClient)
    : ICommandHandler<StoreBasketCommand, StoreBasketResult>
{
    public async Task<StoreBasketResult> Handle(StoreBasketCommand command, CancellationToken cancellationToken)
    {
        await DeductDiscount(command.Cart, cancellationToken);
        
        await repository.StoreBasket(command.Cart, cancellationToken);

        return new StoreBasketResult(command.Cart.UserName);
    }

    private async Task DeductDiscount(ShoppingCart cart, CancellationToken cancellationToken)
    {
        // Communicate with Discount.Grpc and calculate lastest prices of products into sc
        IEnumerable<string> productNames = cart.Items.Select(item => item.ProductName);

        // Create the request object
        var request = new GetMultipleDiscountsRequest
        {
            ProductNames = { productNames }
        };

        // Call the async method
        GetMultipleDiscountsResponse response = await discountClient.GetMultipleDiscountsAsync(
            request, 
            cancellationToken: cancellationToken);

        if (response == null || response.Coupons == null || response.Coupons.Count == 0)
        {
            return; // Or return an appropriate value if this is inside a method
        }

        foreach (var coupon in response.Coupons)
        {
            var item = cart.Items.FirstOrDefault(i => i.ProductName == coupon.ProductName);
            if (item != null)
            {
                item.Price -= coupon.Amount;
            }
        }
    }
}
