using Common.Exceptions;

namespace Basket.API.Exceptions;

public class BasketNotFoundException(string userName) : NotFoundException("Basket", userName)
{
}
