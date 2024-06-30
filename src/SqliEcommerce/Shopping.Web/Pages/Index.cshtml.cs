using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Shopping.Web.Models.Basket;
using Shopping.Web.Models.Catalog;
using Shopping.Web.Services;

namespace Shopping.Web.Pages;
public class IndexModel
    (ICatalogService catalogService, IBasketService basketService, ILogger<IndexModel> logger)
    : PageModel
{    
    public IEnumerable<ProductModel> ProductList { get; set; } = [];
    
    public IEnumerable<ProductModel> LastProductList
    {
        get
        {
            return ProductList.Where(p => p.ImageFile != null).TakeLast(4);
        }
    }

    public async Task<IActionResult> OnGetAsync()
    {
        logger.LogInformation("Index page visited");
        var result = await catalogService.GetProducts();
        //var result = await catalogService.GetProducts(2, 3);
        ProductList = result.Products;
        return Page();
    }

    public async Task<IActionResult> OnPostAddToCartAsync(Guid productId)
    {
        logger.LogInformation("Add to cart button clicked");

        var productResponse = await catalogService.GetProduct(productId);

        var basket = await basketService.LoadUserBasket();

        basket.Items.Add(new ShoppingCartItemModel
        {
            ProductId = productId,
            ProductName = productResponse.Product.Name,
            Price = productResponse.Product.Price,
            Quantity = 1,
            Color = "Black"
        });

        await basketService.StoreBasket(new StoreBasketRequest(basket));
        
        return RedirectToPage("Cart");
    }    
}
