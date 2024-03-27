using AzureMicroservicesTest.Api.Orders.Db;
using AzureMicroservicesTest.Api.Orders.Interfaces;
using AzureMicroservicesTest.Api.Orders.OrderProviders;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddDbContext<OrdersDbContext>(options =>
{
    options.UseInMemoryDatabase("Orders");
});
builder.Services.AddAutoMapper(typeof(Program));
builder.Services.AddScoped<IOrdersProvider, OrderProvider>();
builder.Services.AddControllers();

var app = builder.Build();

// Configure the HTTP request pipeline.

app.UseAuthorization();

app.MapControllers();

app.Run();
