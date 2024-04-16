using MinimalPieShopApi.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
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

pieGroup.MapGet("", ([AsParameters]PieListParameters pageParams) =>
{
    return pieList.Skip((pageParams.PageNumber - 1) * pageParams.PageSize).Take(pageParams.PageSize);
});

const string GetPieRouteName = "GetPie";
pieGroup.MapGet("{id}", (int id) =>
{
    return pieList.FirstOrDefault(p => p.Id == id);
}).WithName(GetPieRouteName);

pieGroup.MapPost("", (Pie pie) =>
{
    pieList.Add(pie);
    return Results.CreatedAtRoute(GetPieRouteName, pie);
});

pieGroup.MapPut("{id}", (int id, Pie pie) =>
{
    var existingPie = pieList.FirstOrDefault(p => p.Id == id);
    if (existingPie == null)
    {
        return Results.NotFound();
    }

    existingPie.Name = pie.Name;
    existingPie.Description = pie.Description;
    existingPie.Category = pie.Category;

    return Results.NoContent();
});

pieGroup.MapDelete("{id}", (int id) =>
{
    var existingPie = pieList.FirstOrDefault(p => p.Id == id);
    if (existingPie == null)
    {
        return Results.NotFound();
    }

    pieList.Remove(existingPie);

    return Results.NoContent();
});

app.Run();

internal record WeatherForecast(DateOnly Date, int TemperatureC, string? Summary)
{
    public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);
}
