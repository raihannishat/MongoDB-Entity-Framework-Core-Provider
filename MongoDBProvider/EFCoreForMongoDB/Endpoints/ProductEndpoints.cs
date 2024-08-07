namespace EFCoreForMongoDB.Endpoints;

public static class ProductEndpoints
{
    public static void MapProductEndpoints(WebApplication app)
    {
        var productGroup = app.MapGroup("/api/products");

        productGroup.MapGet("/", GetProducts);
        productGroup.MapGet("/{id}", GetProduct);
        productGroup.MapGet("/{id}/type", GetProductType);
        productGroup.MapGet("/search", SearchProducts);
        productGroup.MapPost("/", CreateProduct);
        productGroup.MapPut("/{id}", UpdateProduct);
        productGroup.MapDelete("/{id}", DeleteProduct);
    }

    private static async Task<IResult> GetProducts(IProductService productService)
    {
        var products = await productService.GetAllProductsAsync();
        return Results.Ok(products);
    }

    private static async Task<IResult> GetProductType(string productId, IProductService productService)
    {
        var type = await productService.GetProductTypeAsync(productId);
        return Results.Ok(type);
    }

    private static async Task<IResult> GetProduct(string id, IProductService productService)
    {
        var product = await productService.GetProductByIdAsync(id);
        return product == null ? Results.NotFound() : Results.Ok(product);
    }

    private static async Task<IResult> SearchProducts(string name, IProductService productService)
    {
        var products = await productService.SearchProductsAsync(p => p.Name.Contains(name));
        return Results.Ok(products);
    }

    private static async Task<IResult> CreateProduct(Product product, IProductService productService)
    {
        await productService.AddProductAsync(product);
        return Results.Created($"/api/products/{product.Id}", product);
    }

    private static async Task<IResult> UpdateProduct(string id, Product product, IProductService productService)
    {
        if (id != product.Id)
        {
            return Results.BadRequest();
        }

        await productService.UpdateProductAsync(product);
        return Results.NoContent();
    }

    private static async Task<IResult> DeleteProduct(string id, IProductService productService)
    {
        await productService.DeleteProductAsync(id);
        return Results.NoContent();
    }
}

