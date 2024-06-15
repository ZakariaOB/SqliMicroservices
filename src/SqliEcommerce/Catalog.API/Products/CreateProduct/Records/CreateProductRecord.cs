using Common.CQRS;

namespace Catalog.API.Products.CreateProduct.Records;
public record CreateProductRequest(
        string Name,
        List<string> Category,
        string Description,
        string ImageFile,
        decimal Price);

public record CreateProductResponse(Guid Id);

public record CreateProductCommand(
    string Name,
    List<string> Category,
    string Description,
    string ImageFile,
    decimal Price) : ICommand<CreateProductResult>;

public record CreateProductResult(Guid Id);