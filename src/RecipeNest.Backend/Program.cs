using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using RecipeNest.Backend.Data;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddDbContext<AppDbContext>(
    options => options.UseNpgsql(builder.Configuration.GetConnectionString("WebApiDatabase")));

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    //app.MapOpenApi();
    app.UseSwagger();
    app.UseSwaggerUI();
}


app.MapGet("/test", () => "TEST");


app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.Run();
