using Basket.API.Data;
using Basket.API.Data.Repository;
using Basket.API.Models;
using Carter;
using Common.Behaviors;
using Common.Exceptions.Handler;
using HealthChecks.UI.Client;
using Marten;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;

var builder = WebApplication.CreateBuilder(args);
var assembly = typeof(Program).Assembly;
builder.Services.AddCarter();
builder.Services.AddMediatR(config =>
{
    config.RegisterServicesFromAssembly(assembly);
    config.AddOpenBehavior(typeof(ValidationBehavior<,>));
    config.AddOpenBehavior(typeof(LoggingBehavior<,>));
});

//Data Services
builder.Services.AddMarten(opts =>
{
    opts.Connection(builder.Configuration.GetConnectionString("Database")!);
    opts.Schema.For<ShoppingCart>().Identity(x => x.UserName);
}).UseLightweightSessions();

//Cross-Cutting Services
builder.Services.AddExceptionHandler<CustomExceptionHandler>();

builder.Services.AddScoped<IBasketRepository, BasketRepository>();

/*
builder.Services.AddScoped<IBasketRepository>(provider =>
{
    var basketRepository = provider.GetRequiredService<IBasketRepository>();
    return new CachedBasketRepository(basketRepository, provider.GetRequiredService<IDistributedCache>());
});*/

/* This is similar to the lines above*/
builder.Services.Decorate<IBasketRepository, CachedBasketRepository>();
// builder.Services.Decorate<IBasketRepository, TestCachedRepository>();

builder.Services.AddStackExchangeRedisCache(options =>
{
    options.Configuration = builder.Configuration.GetConnectionString("Redis");
    //options.InstanceName = "Basket";
});

// Health checks
builder.Services.AddHealthChecks()
    .AddNpgSql(builder.Configuration.GetConnectionString("Database")!)
    .AddRedis(builder.Configuration.GetConnectionString("Redis")!);

var app = builder.Build();
// Configure the HTTP request pipeline.
app.MapCarter();
app.UseExceptionHandler(options => { });
app.UseHealthChecks("/health",
    new HealthCheckOptions
    {
        ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse
    });

app.Run();
