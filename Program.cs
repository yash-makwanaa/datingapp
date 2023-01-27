using API.Data;
using API.interfaces;
using API.services;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddDbContext<DataContext>(opt =>
{
    opt.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection")); 
});
builder.Services.AddScoped<ITokenService , TokenService>();

var app = builder.Build();

// Configure the HTTP request pipeline.

app.MapControllers();

app.Run();
