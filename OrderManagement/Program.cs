using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using OrderManagement.Contracts.Data;
using OrderManagement.Contracts.Data.Repositories;
using OrderManagement.Core;
using OrderManagement.Infrastructure;
using OrderManagement.Infrastructure.Repositories;
using OrderManagement.Infrastructure.Repositories.Generic;
using OrderManagement.Contracts.Entities;
using System.Reflection;
using OrderManagement.Contracts.Data.Cache;
using OrderManagement.Infrastructure.Cache;

var builder = WebApplication.CreateBuilder(args);
ConfigurationManager config = builder.Configuration;

// Add services to the container.

builder.Services.AddDbContext<SouthWestTradersDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));

});

builder.Services.AddAutoMapper(typeof(Program));

//Add services to the container.

builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();

builder.Services.AddTransient<IProductRepository, ProductRepository>();
builder.Services.AddTransient<IOrderRepository, OrderRepository>();
builder.Services.AddTransient<IStockRepository, StockRepository>();
builder.Services.AddCore();

builder.Services.AddDistributedMemoryCache();
builder.Services.AddTransient(typeof(IDistributedCacheRepository), typeof(DistributedCacheRepository));


builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new OpenApiInfo
    {
        Version = "v1",
        Title = "Order Management API",
        Description = "An ASP.NET Core Web API using Onion Architecture and CQRS, South West Traders, is a fictitious company that sells books which allows you to do the following: \n View a list of products \n Add a product\nRemove a product\nPlace an order\nCancel an order\nComplete an order\nView orders\nSearch order by name\nSearch orders by date\nSearch product by name\nView available stock for a given product",
    });
    // using System.Reflection;
    var xmlFilename = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    options.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, xmlFilename));
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
