namespace EFCoreForMongoDB.Endpoints;

public static class ProductEndpoints
{
    public static void MapProductEndpoints(WebApplication app)
    {
        var productGroup = app.MapGroup("/api/products");

        productGroup.MapGet("/", GetProducts);
        productGroup.MapGet("/{id}", GetProduct);
        productGroup.MapGet("/search", SearchProducts);
        productGroup.MapPost("/", CreateProduct);
        productGroup.MapPut("/{id}", UpdateProduct);
        productGroup.MapDelete("/{id}", DeleteProduct);
    }

    private static async Task<IResult> GetProducts(IUnitOfWork unitOfWork)
    {
        var products = await unitOfWork.Repository<Product>().GetAllAsync();
        return Results.Ok(products);
    }

    private static async Task<IResult> GetProduct(string id, IUnitOfWork unitOfWork)
    {
        var product = await unitOfWork.Repository<Product>().GetByIdAsync(id);
        return product == null ? Results.NotFound() : Results.Ok(product);
    }

    private static async Task<IResult> SearchProducts(string name, IUnitOfWork unitOfWork)
    {
        var products = await unitOfWork.Repository<Product>()
            .FindAsync(p => p.Name.Contains(name));
        
        return Results.Ok(products);
    }

    private static async Task<IResult> CreateProduct(Product product, IUnitOfWork unitOfWork)
    {
        await unitOfWork.Repository<Product>().AddAsync(product);
        await unitOfWork.CommitChangesAsync();
        return Results.Created($"/api/products/{product.Id}", product);
    }

    private static async Task<IResult> UpdateProduct(string id, Product product, IUnitOfWork unitOfWork)
    {
        if (id != product.Id)
        {
            return Results.BadRequest();
        }

        unitOfWork.Repository<Product>().Update(product);
        await unitOfWork.CommitChangesAsync();
        return Results.NoContent();
    }

    private static async Task<IResult> DeleteProduct(string id, IUnitOfWork unitOfWork)
    {
        var product = await unitOfWork.Repository<Product>().GetByIdAsync(id);
        if (product == null)
        {
            return Results.NotFound();
        }

        unitOfWork.Repository<Product>().Delete(product);
        await unitOfWork.CommitChangesAsync();
        return Results.NoContent();
    }
}

