using Microsoft.AspNetCore.StaticFiles;
using Microsoft.EntityFrameworkCore;
using PieShopApi.Filters;
using PieShopApi.Persistence;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<PieShopDbContext>(options =>
    options.UseInMemoryDatabase("PieShopDb")
           .LogTo(Console.WriteLine));

builder.Services.AddScoped(typeof(IPieRepository), typeof(PieRepository));
builder.Services.AddScoped(typeof(IAllergyRepository), typeof(AllergyRepository));

builder.Services.AddSingleton(new FileExtensionContentTypeProvider());

builder.Services.AddAutoMapper(Assembly.GetExecutingAssembly());

builder.Services.AddControllers((options) =>
{
    options.Filters.Add<LoggingFilterAttribute>();
});

builder.Services.AddProblemDetails(
    options =>
        options.CustomizeProblemDetails = ctx =>
        {
            ctx.ProblemDetails.Extensions.Add("version", Assembly.GetExecutingAssembly().GetName().Version);
        }
);

var app = builder.Build();

app.MapControllers();

if(!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler();
}

using (var context = new PieShopDbContext(builder.Services.BuildServiceProvider()
    .GetRequiredService<DbContextOptions<PieShopDbContext>>()))
{
    context.Database.EnsureDeleted();
    context.Database.EnsureCreated();
}

app.Run();
