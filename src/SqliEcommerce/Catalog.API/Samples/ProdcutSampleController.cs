using Microsoft.AspNetCore.Mvc;

namespace Catalog.API.Samples
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductsController : ControllerBase
    {
        private static List<ProductSample> _products = new List<ProductSample>
        {
            new ProductSample(1, "Product1", 10.0),
            new ProductSample(2, "Product2", 20.0)
        };

        [HttpGet]
        public ActionResult<IEnumerable<ProductSample>> GetProducts()
        {
            return Ok(_products);
        }

        [HttpGet("{id}")]
        public ActionResult<ProductSample> GetProduct(int id)
        {
            var product = _products.FirstOrDefault(p => p.Id == id);
            if (product == null)
            {
                return NotFound();
            }
            return Ok(product);
        }

        [HttpPost]
        public ActionResult<ProductSample> CreateProduct(ProductSample product)
        {
            _products.Add(product);
            return CreatedAtAction(nameof(GetProduct), new { id = product.Id }, product);
        }

        [HttpPut("{id}")]
        public IActionResult UpdateProduct(int id, ProductSample updatedProduct)
        {
            var product = _products.FirstOrDefault(p => p.Id == id);
            if (product == null)
            {
                return NotFound();
            }
            product = product with
            {
                Name = updatedProduct.Name,
                Price = updatedProduct.Price
            };
            var index = _products.FindIndex(p => p.Id == id);
            _products[index] = product;
            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteProduct(int id)
        {
            var product = _products.FirstOrDefault(p => p.Id == id);
            if (product == null)
            {
                return NotFound();
            }
            _products.Remove(product);
            return NoContent();
        }

        [HttpGet("search")]
        public ActionResult<IEnumerable<ProductSample>> SearchProducts(string name)
        {
            var results = _products.Where(p => p.Name.Contains(name, System.StringComparison.OrdinalIgnoreCase)).ToList();
            return Ok(results);
        }

        [HttpGet("filter")]
        public ActionResult<IEnumerable<ProductSample>> FilterProducts(double minPrice, double maxPrice)
        {
            var results = _products.Where(p => p.Price >= minPrice && p.Price <= maxPrice).ToList();
            return Ok(results);
        }
    }

    public record ProductSample(int Id, string Name, double Price);
}
