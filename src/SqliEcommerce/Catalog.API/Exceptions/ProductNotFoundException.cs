using Common.Exceptions;

namespace Catalog.API.Exceptions;

public class ProductNotFoundException(Guid Id) : NotFoundException("Product", Id)
{
}
