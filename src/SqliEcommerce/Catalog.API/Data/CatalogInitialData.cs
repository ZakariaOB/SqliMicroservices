using Catalog.API.Models;
using Marten;
using Marten.Schema;

namespace Catalog.API.Data;

public class CatalogInitialData : IInitialData
{
    public async Task Populate(IDocumentStore store, CancellationToken cancellation)
    {
        using var session = store.LightweightSession();

        await PopulateProducts(session);

        await PopulateCategories(session);
    }

    private static async Task PopulateProducts(IDocumentSession session)
    {
        if (await session.Query<Product>().AnyAsync())
        {
            return;
        }

        // Marten UPSERT will cater for existing records
        session.Store(GetPreconfiguredProducts());

        await session.SaveChangesAsync();
    }

    private static async Task PopulateCategories(IDocumentSession session)
    {
        if (await session.Query<Category>().AnyAsync())
        {
            return;
        }

        // Marten UPSERT will cater for existing records
        session.Store(GetPreconfiguredCategories());

        await session.SaveChangesAsync();
    }

    private static IEnumerable<Product> GetPreconfiguredProducts() =>
    [
        new Product()
        {
            Id = new Guid("5334c996-8457-4cf0-815c-ed2b77c4ff61"),
            Name = "IPhone X",
            Description = "This phone is the company's biggest change to its flagship smartphone in years. It includes a borderless display.",
            ImageFile = "product-1.png",
            Price = 950.00M,
            Category = new List<string> { "Smart Phone" },
            Attributes = new Dictionary<string, object>
            {
                { "Brand", "Apple" },
                { "Model", "iPhone X" },
                { "Color", "Space Gray" },
                { "Memory", "64GB" },
                { "BatteryLife", "Up to 21 hours talk time" }
            }
        },
        new Product()
        {
            Id = new Guid("c67d6323-e8b1-4bdf-9a75-b0d0d2e7e914"),
            Name = "Samsung Galaxy S10",
            Description = "This phone is the company's biggest change to its flagship smartphone in years. It includes a borderless display.",
            ImageFile = "product-2.png",
            Price = 840.00M,
            Category = new List<string> { "Smart Phone" },
            Attributes = new Dictionary<string, object>
            {
                { "Brand", "Samsung" },
                { "Model", "Galaxy S10" },
                { "Color", "Prism White" },
                { "Memory", "128GB" },
                { "BatteryLife", "Up to 24 hours talk time" }
            }
        },
        new Product()
        {
            Id = new Guid("4f136e9f-ff8c-4c1f-9a33-d12f689bdab8"),
            Name = "Huawei P30 Pro",
            Description = "This phone is the company's biggest change to its flagship smartphone in years. It includes a borderless display.",
            ImageFile = "product-3.png",
            Price = 650.00M,
            Category = new List<string> { "Smart Phone" },
            Attributes = new Dictionary<string, object>
            {
                { "Brand", "Huawei" },
                { "Model", "P30 Pro" },
                { "Color", "Aurora" },
                { "Memory", "256GB" },
                { "BatteryLife", "Up to 22 hours talk time" }
            }
        },
        new Product()
        {
            Id = new Guid("6ec1297b-ec0a-4aa1-be25-6726e3b51a27"),
            Name = "Xiaomi Mi 9",
            Description = "This phone is the company's biggest change to its flagship smartphone in years. It includes a borderless display.",
            ImageFile = "product-4.png",
            Price = 470.00M,
            Category = new List<string> { "Smart Phone" },
            Attributes = new Dictionary<string, object>
            {
                { "Brand", "Xiaomi" },
                { "Model", "Mi 9" },
                { "Color", "Ocean Blue" },
                { "Memory", "128GB" },
                { "BatteryLife", "Up to 18 hours talk time" }
            }
        },
        new Product()
        {
            Id = new Guid("b786103d-c621-4f5a-b498-23452610f88c"),
            Name = "HTC U11+ Plus",
            Description = "This phone is the company's biggest change to its flagship smartphone in years. It includes a borderless display.",
            ImageFile = "product-5.png",
            Price = 380.00M,
            Category = new List<string> { "Smart Phone" },
            Attributes = new Dictionary<string, object>
            {
                { "Brand", "HTC" },
                { "Model", "U11+ Plus" },
                { "Color", "Amazing Silver" },
                { "Memory", "128GB" },
                { "BatteryLife", "Up to 25 hours talk time" }
            }
        },
        new Product()
        {
            Id = new Guid("c4bbc4a2-4555-45d8-97cc-2a99b2167bff"),
            Name = "LG G7 ThinQ",
            Description = "This phone is the company's biggest change to its flagship smartphone in years. It includes a borderless display.",
            ImageFile = "product-6.png",
            Price = 240.00M,
            Category = new List<string> { "Smart Phone" },
            Attributes = new Dictionary<string, object>
            {
                { "Brand", "LG" },
                { "Model", "G7 ThinQ" },
                { "Color", "New Platinum Gray" },
                { "Memory", "64GB" },
                { "BatteryLife", "Up to 23 hours talk time" }
            }
        },
        new Product()
        {
            Id = new Guid("93170c85-7795-489c-8e8f-7dcf3b4f4188"),
            Name = "Panasonic Lumix",
            Description = "This camera features a high-resolution sensor and advanced features for professional photography.",
            ImageFile = "product-7.png",
            Price = 240.00M,
            Category = new List<string> { "Camera" },
            Attributes = new Dictionary<string, object>
            {
                { "Brand", "Panasonic" },
                { "Model", "Lumix DMC-G85" },
                { "Color", "Black" },
                { "Resolution", "16MP" },
                { "Lens", "12-60mm" },
                { "BatteryLife", "Up to 330 shots" }
            }
        }
    ];

    private static IEnumerable<Category> GetPreconfiguredCategories() =>
    [
        new Category
        {
            Id = Guid.NewGuid(),
            Name = "Smart Phones",
            Description = "Mobile devices including the latest smartphones from top brands."
        },
        new Category
        {
            Id = Guid.NewGuid(),
            Name = "Home Appliances",
            Description = "Appliances for home and kitchen, including refrigerators, ovens, and more."
        },
        new Category
        {
            Id = Guid.NewGuid(),
            Name = "Cameras",
            Description = "Photography equipment including DSLRs, mirrorless cameras, and accessories."
        },
        new Category
        {
            Id = Guid.NewGuid(),
            Name = "Laptops",
            Description = "Personal and professional laptops from leading brands."
        },
        new Category
        {
            Id = Guid.NewGuid(),
            Name = "Wearable Devices",
            Description = "Wearable technology including smartwatches and fitness trackers."
        },
        new Category
        {
            Id = Guid.NewGuid(),
            Name = "Audio Equipment",
            Description = "High-quality audio equipment including headphones, speakers, and sound systems."
        },
        new Category
        {
            Id = Guid.NewGuid(),
            Name = "Gaming Consoles",
            Description = "Latest gaming consoles and accessories."
        }
    ];

}
