var builder = WebApplication.CreateBuilder(args);

// Add services to the container
builder.Services.AddControllers();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Register dbcontext for mongodb
var dbSettings = builder.Configuration.GetSection(typeof(MongoDbSettings).Name).Get<MongoDbSettings>();
builder.Services.AddDbContext<MongoDbContext>(options =>
    options.UseMongoDB(dbSettings!.AtlasURI, dbSettings.DatabaseName));

// Register UnitOfWork and GetRepository with other services
builder.Services.AddScoped<IDbContext, MongoDbContext>();
builder.Services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
builder.Services.AddScoped<IProductService, ProductService>();  
builder.Services.AddScoped<ICategoryService, CategoryService>();

var app = builder.Build();

// Add custom endpoints
ProductEndpoints.MapProductEndpoints(app);
CategoryEndpoints.MapCategoryEndpoints(app);

// Configure the HTTP request pipeline
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
