
using Carter;
using Catalog.API.Models;
using Mapster;
using MediatR;

namespace Catalog.API.Products.GetProductByCategory;

public record GetProductByCategoriesResponse(IEnumerable<Product> Products);

public class GetProductByCategoriesEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapGet("/products/categories",
        async (HttpRequest request, ISender sender) =>
        {
            var categories = request.Query["categories"].ToString().Split(',').ToList();
            var result = await sender.Send(new GetProductsByCategoriesQuery(categories));

            var response = result.Adapt<GetProductByCategoriesResponse>();

            return Results.Ok(response);
        })
        .WithName("GetProductsByCategories")
        .Produces<GetProductByCategoriesResponse>(StatusCodes.Status200OK)
        .ProducesProblem(StatusCodes.Status400BadRequest)
        .WithSummary("Get Products By Categories")
        .WithDescription("Get Products By Categories");
    }
}
