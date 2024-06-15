using Carter;
using Catalog.API.Data;
using Common.Behaviors;
using Common.Exceptions.Handler;
using FluentValidation;
using HealthChecks.UI.Client;
using Marten;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;

var builder = WebApplication.CreateBuilder(args);

var assembly = typeof(Program).Assembly;
builder.Services.AddMediatR(config =>
{
    config.RegisterServicesFromAssembly(assembly);
    config.AddOpenBehavior(typeof(ValidationBehavior<,>));
    config.AddOpenBehavior(typeof(LoggingBehavior<,>));
});

builder.Services.AddValidatorsFromAssembly(assembly);

builder.Services.AddCarter();

builder.Services.AddExceptionHandler<CustomExceptionHandler>();
builder.Services.AddProblemDetails();

builder.Services.AddMarten(opts =>
{
    opts.Connection(builder.Configuration.GetConnectionString("MartenDatabase")!);
}).UseLightweightSessions();

if (builder.Environment.IsDevelopment()) 
{
    builder.Services.InitializeMartenWith<CatalogInitialData>();
}

builder.Services
    .AddHealthChecks()
    .AddNpgSql(builder.Configuration.GetConnectionString("MartenDatabase")!);

var app = builder.Build();

// Configure the HTTP request pipeline.
app.MapCarter();

app.UseHealthChecks("/health",
    new HealthCheckOptions
    {
        ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse
    });


app.Run();
