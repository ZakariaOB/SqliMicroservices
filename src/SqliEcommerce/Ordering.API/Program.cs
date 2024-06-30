using Ordering.Infrastructure.Data.Extensions;
using Ordering.Infrastructure;
using Ordering.Application;
using Ordering.API;

var builder = WebApplication.CreateBuilder(args);

builder.Services
    .AddApplicationServices(builder.Configuration)
    .AddInfrastructureServices(builder.Configuration)
    .AddApiServices(builder.Configuration);

// Swagger
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new()
    {
        Title = "Ordering API",
        Version = "v1"
    });
});

var app = builder.Build();

app.UseApiServices();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c => c.SwaggerEndpoint(
        "/swagger/v1/swagger.json",
        "v1"));
    await app.InitialiseDatabaseAsync();
}

app.Run();
