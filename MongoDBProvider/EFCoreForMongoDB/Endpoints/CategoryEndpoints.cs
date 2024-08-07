namespace EFCoreForMongoDB.Endpoints;

public static class CategoryEndpoints
{
    public static void MapCategoryEndpoints(WebApplication app)
    {
        var categoryGroup = app.MapGroup("/api/categories");

        categoryGroup.MapGet("/", GetCategoriesAsync);
        categoryGroup.MapGet("/{id}", GetCategoryAsync);
        categoryGroup.MapPost("/", CreateCategoryAsync);
        categoryGroup.MapPut("/{id}", UpdateCategoryAsync);
        categoryGroup.MapDelete("/{id}", DeleteCategoryAsync);
        categoryGroup.MapGet("/search", SearchCategoriesAsync);
    }

    private static async Task<IResult> GetCategoriesAsync(ICategoryService categoryService)
    {
        var categories = await categoryService.GetAllCategoriesAsync();
        return Results.Ok(categories);
    }

    private static async Task<IResult> GetCategoryAsync(string id, ICategoryService categoryService)
    {
        var category = await categoryService.GetCategoryByIdAsync(id);
        return category == null ? Results.NotFound() : Results.Ok(category);
    }

    private static async Task<IResult> CreateCategoryAsync(Category category, ICategoryService categoryService)
    {
        await categoryService.AddCategoryAsync(category);
        return Results.Created($"/api/categories/{category.Id}", category);
    }

    private static async Task<IResult> UpdateCategoryAsync(string id, Category category, ICategoryService categoryService)
    {
        if (id != category.Id)
        {
            return Results.BadRequest();
        }

        await categoryService.UpdateCategoryAsync(category);
        return Results.NoContent();
    }

    private static async Task<IResult> DeleteCategoryAsync(string id, ICategoryService categoryService)
    {
        await categoryService.DeleteCategoryAsync(id);
        return Results.NoContent();
    }

    private static async Task<IResult> SearchCategoriesAsync(string name, ICategoryService categoryService)
    {
        var categories = await categoryService.SearchCategoriesAsync(c => c.Name.Contains(name));
        return Results.Ok(categories);
    }
}

