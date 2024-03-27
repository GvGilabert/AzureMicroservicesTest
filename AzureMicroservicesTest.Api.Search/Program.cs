using AzureMicroservicesTest.Api.Search.Interfaces;
using AzureMicroservicesTest.Api.Search.Services;
using Polly;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var httpClientRetryPolicy = (PolicyBuilder<HttpResponseMessage> p) => p.WaitAndRetryAsync(5, _ => TimeSpan.FromMilliseconds(1000));

builder.Services.AddHttpClient("OrdersService", config =>
{
    config.BaseAddress = new Uri(builder.Configuration["Services:Orders"]!);
}).AddTransientHttpErrorPolicy(httpClientRetryPolicy);

builder.Services.AddHttpClient("ProductService", config =>
{
    config.BaseAddress = new Uri(builder.Configuration["Services:Products"]!);
}).AddTransientHttpErrorPolicy(httpClientRetryPolicy);

builder.Services.AddHttpClient("CustomerService", config =>
{
    config.BaseAddress = new Uri(builder.Configuration["Services:Customers"]!);
}).AddTransientHttpErrorPolicy(httpClientRetryPolicy);


builder.Services.AddScoped<ISearchService, SearchService>();
builder.Services.AddScoped<IOrdersService, OrderService>();
builder.Services.AddScoped<IProductsService, ProductService>();
builder.Services.AddScoped<ICustomersService, CustomerService>();

builder.Services.AddControllers();


var app = builder.Build();

// Configure the HTTP request pipeline.

app.UseAuthorization();

app.MapControllers();

app.Run();
