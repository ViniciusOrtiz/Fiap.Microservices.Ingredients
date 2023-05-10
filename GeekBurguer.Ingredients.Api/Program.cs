using GeekBurguer.Ingredients.Api.Extensions;
using GeekBurguer.Ingredients.Api.Infra;
using GeekBurguer.Ingredients.Api.Repository;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddScoped<ApiClient>();
builder.Services.AddScoped<IProductsRequestRepository, ProductsRequestRepository>();
builder.Services.AddScoped<IIngredientsRepository, IngredientsRepository>();
builder.Services.AddScoped<IServiceBusLabelLoader, ServiceBusLabelLoader>();

builder.Services.AddDbContext<IngredientsContext>(options =>
    options.UseSqlite("Data source=database.db"));

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddControllers();

var app = builder.Build();
using var scope = app.Services.CreateScope();

var services = scope.ServiceProvider.GetRequiredService<IProductsRequestRepository>();
//await services.GetProducts();

app.UseSwagger();
app.UseSwaggerUI();

app.UseRouting();
app.UseEndpoints(endpoints =>
{
    endpoints.MapControllers();
});

var labelLoaderService = scope.ServiceProvider.GetRequiredService<IServiceBusLabelLoader>();
await labelLoaderService.Run(builder.Configuration);

app.Run();