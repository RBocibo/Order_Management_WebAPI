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
using Microsoft.AspNetCore.Authentication.JwtBearer;
using System.IdentityModel.Tokens.Jwt;
using OrderManagement.Core.Authorization;
using Microsoft.AspNetCore.Authorization;
using Swashbuckle.AspNetCore.SwaggerUI;

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


builder.Services.AddEndpointsApiExplorer();

JwtSecurityTokenHandler.DefaultInboundClaimTypeMap.Clear();

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
          .AddJwtBearer(options =>
          {
              // base-address of identityserver
              options.Authority = "https://southwesttraderssts.azurewebsites.net";
              // name of the API resource
              options.Audience = "https://southwesttraderssts.azurewebsites.net/resources";
          });

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
    var xmlFilenameInfrastructure = $"{Assembly.Load("OrderManagement.Infrastructure").GetName().Name}.xml";
    var xmlFilenameCore = $"{Assembly.Load("OrderManagement.Core").GetName().Name}.xml";

    options.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, xmlFilename));
    options.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, xmlFilenameInfrastructure));
    options.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, xmlFilenameCore));

    //swagger documentation
    options.EnableAnnotations();

    // Define the OAuth2.0 scheme that's in use (i.e. Implicit Flow)
    options.AddSecurityDefinition("oauth2", new OpenApiSecurityScheme
    {
        Type = SecuritySchemeType.OAuth2,
        Flows = new OpenApiOAuthFlows
        {
            Implicit = new OpenApiOAuthFlow
            {
                AuthorizationUrl = new Uri("https://southwesttraderssts.azurewebsites.net/connect/authorize", UriKind.Absolute),
                Scopes = new Dictionary<string, string>
                {
                    { "read", "Access identity information" },
                    { "roles", "Access API roles" }
                }
            }
        }
    });

    options.AddSecurityRequirement(new OpenApiSecurityRequirement
        {
          {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference { Type = ReferenceType.SecurityScheme, Id = "oauth2" }
            },
            new[] { "read", "roles" }
         }
    });

    
});

builder.Services.AddAuthorization(options =>
     options.AddPolicy(Policy.AdminAuthorizePolicy,
     requirement => requirement
           .AddRequirements(
             new AdminAuthorizeRequirement("Admin"))
           .RequireClaim(JwtClaimNames.Sub)
           ));

builder.Services.AddScoped<IAuthorizationHandler, AdminAuthorizeHandler>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(options =>
    {

        options.DocExpansion(DocExpansion.None);
        options.DisplayRequestDuration();
        options.DefaultModelRendering(ModelRendering.Model);

        options.EnableFilter();
        // options.DefaultModelExpandDepth(5);
        options.DefaultModelExpandDepth(-1);

        options.OAuthClientId("southwest.traders");
        options.OAuthClientSecret("secret");
        options.OAuthRealm("South West Traders");
        options.OAuthAppName("South West Traders");
    });
}

app.UseReDoc(options =>
{
    options.DocumentTitle = "South West Traders Order Management API";
    options.SpecUrl = "/swagger/v1/swagger.json";
});

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
