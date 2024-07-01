using Basket.API.Data;
using Basket.API.Models;
using Carter;
using Common.Behaviors;
using Common.Messaging.MassTransit;
using Common.Exceptions.Handler;
using Discount.Grpc;
using HealthChecks.UI.Client;
using Marten;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Basket.API.Data.Repository;
using Basket.API.Options;
using System.Runtime.Caching;

var builder = WebApplication.CreateBuilder(args);
var assembly = typeof(Program).Assembly;
builder.Services.AddCarter();
builder.Services.AddMediatR(config =>
{
    config.RegisterServicesFromAssembly(assembly);
    config.AddOpenBehavior(typeof(ValidationBehavior<,>));
    config.AddOpenBehavior(typeof(LoggingBehavior<,>));
});

//Async Communication Services
builder.Services.AddMessageBroker(builder.Configuration);

//Data Services
builder.Services.AddMarten(opts =>
{
    opts.Connection(builder.Configuration.GetConnectionString("Database")!);
    opts.Schema.For<ShoppingCart>().Identity(x => x.UserName);
}).UseLightweightSessions();

//Cross-Cutting Services
builder.Services.AddExceptionHandler<CustomExceptionHandler>();

builder.Services.AddScoped<IBasketRepository, BasketRepository>();

//Grpc Services
builder.Services.AddGrpcClient<DiscountProtoService.DiscountProtoServiceClient>(options =>
{
    options.Address = new Uri(builder.Configuration["GrpcSettings:DiscountUrl"]!);
})
.ConfigurePrimaryHttpMessageHandler(() =>
{
    var handler = new HttpClientHandler
    {
        ServerCertificateCustomValidationCallback =
        HttpClientHandler.DangerousAcceptAnyServerCertificateValidator
    };

    return handler;
});

builder.Services.AddSingleton<ObjectCache>(MemoryCache.Default);

var cachingSettings = new CachingSettings();
builder.Configuration.GetSection(nameof(CachingSettings)).Bind(cachingSettings);

/*
builder.Services.AddScoped<IBasketRepository>(provider =>
{
    var basketRepository = provider.GetRequiredService<IBasketRepository>();
    return new CachedBasketRepository(basketRepository, provider.GetRequiredService<IDistributedCache>());
});*/

/* This is similar to the lines above*/
//builder.Services.Decorate<IBasketRepository, CachedBasketRepository>();
/*builder.Services.Decorate<IBasketRepository, TestCachedRepository>();*/


// Decorate the repository based on the caching strategy
switch (cachingSettings.Strategy)
{
    case CachingStrategy.Redis:
        builder.Services.Decorate<IBasketRepository, CachedBasketRepository>();
        break;
    case CachingStrategy.InMemory:
        builder.Services.Decorate<IBasketRepository, InMemoryCachedRepository>();
        break;
    case CachingStrategy.Test:
        builder.Services.Decorate<IBasketRepository, TestCachedRepository>();
        break;
    default:
        throw new Exception($"Unsupported caching strategy: {cachingSettings.Strategy}");
}


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
