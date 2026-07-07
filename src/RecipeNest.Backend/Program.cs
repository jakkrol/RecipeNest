using Microsoft.EntityFrameworkCore;
using RecipeNest.Backend.Data;
using RecipeNest.Backend.Services;
using Scalar.AspNetCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddDbContext<AppDbContext>(
    options => options.UseNpgsql(builder.Configuration.GetConnectionString("WebApiDatabase")));

builder.Services.AddSingleton<HashingService>();

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddOpenApi();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.MapScalarApiReference();
    app.UseSwagger();
    app.UseSwaggerUI();
}


//app.MapGet("/test", () => "TEST");
app.MapGet("/", () => "Hello");

//app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.Run();
