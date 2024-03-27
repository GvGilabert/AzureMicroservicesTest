using AzureMicroservicesTest.Api.Customers.Db;
using AzureMicroservicesTest.Api.Customers.Interfaces;
using AzureMicroservicesTest.Api.Customers.Providers;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddScoped<ICustomerProvider, CustomerProvider>();
builder.Services.AddControllers();
builder.Services.AddAutoMapper(typeof(Program));
builder.Services.AddDbContext<CustomersDbContext>(options => 
{
    options.UseInMemoryDatabase("Customers");
});

var app = builder.Build();

// Configure the HTTP request pipeline.

app.UseAuthorization();

app.MapControllers();

app.Run();
