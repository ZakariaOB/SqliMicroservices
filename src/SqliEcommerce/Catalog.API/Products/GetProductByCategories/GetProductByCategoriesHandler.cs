
using Catalog.API.Models;
using Common.CQRS;
using Marten;

namespace Catalog.API.Products.GetProductByCategory;

public record GetProductsByCategoriesQuery(List<string> Categories) : IQuery<GetProductByCategoriesResult>;
public record GetProductByCategoriesResult(IEnumerable<Product> Products);

internal class GetProductByCategoriesQueryHandler
    (IDocumentSession session)
    : IQueryHandler<GetProductsByCategoriesQuery, GetProductByCategoriesResult>
{

    public async Task<GetProductByCategoriesResult> Handle(GetProductsByCategoriesQuery request, CancellationToken cancellationToken)
    {
        var products = await session.Query<Product>()
                .Where(p => p.Category.Any(c => request.Categories.Contains(c)))
                .ToListAsync(cancellationToken);

        return new GetProductByCategoriesResult(products);
    }
}
