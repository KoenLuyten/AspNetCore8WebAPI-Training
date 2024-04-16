using AutoMapper;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MinimalPieShopApi.Endpoints;
using MinimalPieShopApi.Models;
using MinimalPieShopApi.Persistence;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddDbContext<PieShopDbContext>(options =>
    options.UseInMemoryDatabase("PieShopDb")
           .LogTo(Console.WriteLine));

builder.Services.AddScoped(typeof(IAsyncRepository<>), typeof(RepositoryBase<>));
builder.Services.AddScoped<IPieRepository, PieRepository>();

builder.Services.AddAutoMapper(Assembly.GetExecutingAssembly());

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.MapPieEndpoints();

using (var context = new PieShopDbContext(builder.Services.BuildServiceProvider()
    .GetRequiredService<DbContextOptions<PieShopDbContext>>()))
{
    context.Database.EnsureDeleted();
    context.Database.EnsureCreated();
}

app.Run();
