namespace Catalog.API.Samples
{
    public static class MapProductSampleSampleUsingMinimalApi
    {
        public static void MapProductSampleEndpoints(this IEndpointRouteBuilder app)
        {
            var products = new List<ProductSample>
            {
                new (1, "Product1",10.0 ),
                new (1, "Product 2",10.0 )
            };

            app.MapGet("/productsamples", () => products);

            app.MapGet("/productsamples/{id}", (int id) =>
            {
                var product = products.FirstOrDefault(p => p.Id == id);
                return product != null ? Results.Ok(product) : Results.NotFound();
            });

            app.MapPost("/productsamples", (ProductSample product) =>
            {
                products.Add(product);
                return Results.Created($"/productsamples/{product.Id}", product);
            });

            app.MapPut("/productsamples/{id}", (int id, ProductSample updatedProductSample) =>
            {
                var product = products.FirstOrDefault(p => p.Id == id);
                if (product == null) return Results.NotFound();
                var updatedProduct = product with
                {
                    Name = updatedProductSample.Name,
                    Price = updatedProductSample.Price
                };
                return Results.NoContent();
            });

            app.MapDelete("/productsamples/{id}", (int id) =>
            {
                var product = products.FirstOrDefault(p => p.Id == id);
                if (product == null) return Results.NotFound();
                products.Remove(product);
                return Results.NoContent();
            });
        }
    }
}
