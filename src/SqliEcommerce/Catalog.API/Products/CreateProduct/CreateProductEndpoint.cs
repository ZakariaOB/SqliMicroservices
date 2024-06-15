using Carter;
using Catalog.API.Products.CreateProduct.Records;
using Mapster;
using MediatR;

namespace Catalog.API.Products.CreateProduct;

public class CreateProductEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapPost("/products",
            async (CreateProductRequest request, ISender sender) =>
            {
                return await HandleCreateProduct(request, sender);
            })
           .WithName("CreateProduct")
           .Produces<CreateProductResponse>(StatusCodes.Status201Created)
           .ProducesProblem(StatusCodes.Status400BadRequest)
           .WithSummary("Create Product")
           .WithDescription("Creates a new product with the provided details.");
    }

    private static async Task<IResult> HandleCreateProduct(CreateProductRequest request, ISender sender)
    {
        CreateProductCommand command = request.Adapt<CreateProductCommand>();

        CreateProductResult result = await sender.Send(command);
        
        CreateProductResponse response = result.Adapt<CreateProductResponse>();

        return Results.Created($"/products/{response.Id}", response);
    }
}
