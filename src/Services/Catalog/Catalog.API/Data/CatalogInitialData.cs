using Marten.Schema;

namespace Catalog.API.Data;

public class CatalogInitialData : IInitialData
{
    public async Task Populate(IDocumentStore store, CancellationToken cancellation)
    {
        using var session = store.LightweightSession();

        if (await session.Query<Product>().AnyAsync())
            return;

        session.Store<Product>(GetPreconfiguredProducts());
        await session.SaveChangesAsync();
    }

    private static IEnumerable<Product> GetPreconfiguredProducts() => new List<Product>()
    {
        new Product()
        {
            Id = Guid.NewGuid(),
            Name = "Product #1",
            Description = "Description #1",
            ImageFile = "item-1.png",
            Price = 100.00M,
            Category = new List<string> { "category #1" }
        },
        new Product()
        {
            Id = Guid.NewGuid(),
            Name = "Product #2",
            Description = "Description #2",
            ImageFile = "item-2.png",
            Price = 200.00M,
            Category = new List<string> { "category #1", "category #2" }
        },
        new Product()
        {
            Id = Guid.NewGuid(),
            Name = "Product #3",
            Description = "Description #3",
            ImageFile = "item-3.png",
            Price = 300.00M,
            Category = new List<string> { "category #2", "category #3" }
        },
        new Product()
        {
            Id = Guid.NewGuid(),
            Name = "Product #4",
            Description = "Description #4",
            ImageFile = "item-4.png",
            Price = 400.00M,
            Category = new List<string> { "category #4" }
        }
    };

}
