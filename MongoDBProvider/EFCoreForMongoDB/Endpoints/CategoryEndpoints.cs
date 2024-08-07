namespace EFCoreForMongoDB.Endpoints;

public static class CategoryEndpoints
{
    public static void MapCategoryEndpoints(WebApplication app)
    {
        var categoryGroup = app.MapGroup("/api/categories");

        categoryGroup.MapGet("/", GetCategories);
        categoryGroup.MapGet("/{id}", GetCategory);
        categoryGroup.MapPost("/", CreateCategory);
        categoryGroup.MapPut("/{id}", UpdateCategory);
        categoryGroup.MapDelete("/{id}", DeleteCategory);
        categoryGroup.MapGet("/search", SearchCategories);
    }

    private static async Task<IResult> GetCategories(ICategoryService categoryService)
    {
        var categories = await categoryService.GetAllCategoriesAsync();
        return Results.Ok(categories);
    }

    private static async Task<IResult> GetCategory(string id, ICategoryService categoryService)
    {
        var category = await categoryService.GetCategoryByIdAsync(id);
        return category == null ? Results.NotFound() : Results.Ok(category);
    }

    private static async Task<IResult> CreateCategory(Category category, ICategoryService categoryService)
    {
        await categoryService.AddCategoryAsync(category);
        return Results.Created($"/api/categories/{category.Id}", category);
    }

    private static async Task<IResult> UpdateCategory(string id, Category category, ICategoryService categoryService)
    {
        if (id != category.Id)
        {
            return Results.BadRequest();
        }

        await categoryService.UpdateCategoryAsync(category);
        return Results.NoContent();
    }

    private static async Task<IResult> DeleteCategory(string id, ICategoryService categoryService)
    {
        await categoryService.DeleteCategoryAsync(id);
        return Results.NoContent();
    }

    private static async Task<IResult> SearchCategories(string name, ICategoryService categoryService)
    {
        var categories = await categoryService.SearchCategoriesAsync(c => c.Name.Contains(name));
        return Results.Ok(categories);
    }
}

