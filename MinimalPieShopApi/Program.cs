using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MinimalPieShopApi.Models;
using MinimalPieShopApi.Persistence;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddDbContext<PieShopDbContext>(options =>
    options.UseInMemoryDatabase("PieShopDb")
           .LogTo(Console.WriteLine));

builder.Services.AddScoped(typeof(IAsyncRepository<>), typeof(RepositoryBase<>));
builder.Services.AddScoped<IPieRepository, PieRepository>();


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

var pieList = new List<Pie>
{
    new Pie { Id = 1, Name = "Apple Pie", Description = "Tasty", Category="Fruit" },
    new Pie { Id = 2, Name = "Cherry Pie", Description = "Yummy", Category = "Fruit" },
    new Pie { Id = 3, Name = "Pumpkin Pie", Description = "Delicious", Category = "Vegetable" }
};

var pieGroup = app.MapGroup("/pies/")
                  .WithTags("Pie Endpoints");

//pieGroup.MapGet("", () =>
//{
//    return pieList;
//});

//pieGroup.MapGet("", (int pageNumber = 1, int pageSize = 10) =>
//{
//    return pieList.Skip((pageNumber - 1) * pageSize).Take(pageSize);
//});

pieGroup.MapGet("", async (string? category, string? searchTerm, [AsParameters] PieListParameters pageParams, [FromServices] IPieRepository repository) =>
{
    return await repository.ListPiesAsync(category, searchTerm, pageParams);
});

const string GetPieRouteName = "GetPie";
pieGroup.MapGet("{id}", async (int id, IPieRepository repository) =>
{
    return await repository.GetByIdAsync(id);
}).WithName(GetPieRouteName);

pieGroup.MapPost("", async (Pie pie, IPieRepository repository) =>
{
    var savedPie = await repository.AddAsync(pie);

    return Results.CreatedAtRoute(GetPieRouteName, savedPie);
});

pieGroup.MapPut("{id}", async (int id, Pie pie, IPieRepository repository) =>
{
    var existingPie = await repository.GetByIdAsync(id);
    if (existingPie == null)
    {
        return Results.NotFound();
    }

    existingPie.Name = pie.Name;
    existingPie.Description = pie.Description;
    existingPie.Category = pie.Category;

    await repository.UpdateAsync(existingPie);

    return Results.NoContent();
});

pieGroup.MapDelete("{id}", async (int id, IPieRepository repository) =>
{
    var existingPie = await repository.GetByIdAsync(id);
    if (existingPie == null)
    {
        return Results.NotFound();
    }

    await repository.DeleteAsync(existingPie);

    return Results.NoContent();
});

using (var context = new PieShopDbContext(builder.Services.BuildServiceProvider()
    .GetRequiredService<DbContextOptions<PieShopDbContext>>()))
{
    context.Database.EnsureDeleted();
    context.Database.EnsureCreated();
}

app.Run();
