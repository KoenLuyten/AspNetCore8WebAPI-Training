using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
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

pieGroup.MapGet("", async (string? category, string? searchTerm, [AsParameters] PieListParameters pageParams, [FromServices] IPieRepository repository, [FromServices] IMapper mapper) =>
{
    var result = await repository.ListPiesAsync(category, searchTerm, pageParams);

    return mapper.Map<IEnumerable<PieForListDto>>(result);
});

const string GetPieRouteName = "GetPie";
pieGroup.MapGet("{id}", async (int id, IPieRepository repository, [FromServices] IMapper mapper) =>
{
    var pie = await repository.GetByIdAsync(id);

    return mapper.Map<PieDto>(pie);
}).WithName(GetPieRouteName);

pieGroup.MapPost("", async (PieForCreationDto pie, IPieRepository repository, [FromServices] IMapper mapper) =>
{
    var pieToCreate = mapper.Map<Pie>(pie);

    var savedPie = await repository.AddAsync(pieToCreate);

    var pieDto = mapper.Map<PieDto>(savedPie);

    return Results.CreatedAtRoute(GetPieRouteName, pieDto);
});

pieGroup.MapPut("{id}", async (int id, PieForUpdateDto pie, IPieRepository repository, [FromServices] IMapper mapper) =>
{
    var existingPie = await repository.GetByIdAsync(id);
    if (existingPie == null)
    {
        return Results.NotFound();
    }

    mapper.Map(pie, existingPie);

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
