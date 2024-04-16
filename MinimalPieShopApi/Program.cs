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

app.MapGet("/pies", () =>
{
    return pieList;
});

const string GetPieRouteName = "GetPie";
app.MapGet("/pies/{id}", (int id) =>
{
    return pieList.FirstOrDefault(p => p.Id == id);
}).WithName(GetPieRouteName);

app.MapPost("/pies", (Pie pie) =>
{
    pieList.Add(pie);
    return Results.CreatedAtRoute(GetPieRouteName, pie);
});

app.MapPut("/pies/{id}", (int id, Pie pie) =>
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

app.MapDelete("/pies/{id}", (int id) =>
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
