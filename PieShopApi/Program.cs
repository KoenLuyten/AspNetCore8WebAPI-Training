using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;
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
builder.Services.AddApplicationInsightsTelemetry();

var app = builder.Build();

app.MapControllers();

if(!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler(options =>
    {
        options.Run(context =>
        {
            context.Response.StatusCode = 500;
            context.Response.ContentType = "application/json";

            var errorFeature = context.Features.Get<IExceptionHandlerFeature>();
            if (errorFeature != null)
            {
                if (errorFeature.Error is NotImplementedException)
                {
                    context.Response.StatusCode = 501;

                    var problemDetails = new ProblemDetails
                    {
                        Status = 501,
                        Title = "Not Implemented",
                        Detail = errorFeature.Error.Message,
                        Instance = context.Request.Path
                    };

                    return context.Response.WriteAsJsonAsync(problemDetails);
                }
                else
                {
                    var problemDetails = new ProblemDetails
                    {
                        Status = 500,
                        Title = "An error occurred",
                        Detail = errorFeature.Error.Message,
                        Instance = context.Request.Path
                    };

                    return context.Response.WriteAsJsonAsync(problemDetails);
                }
            }

            return Task.CompletedTask;
        });
    });
}

using (var context = new PieShopDbContext(builder.Services.BuildServiceProvider()
    .GetRequiredService<DbContextOptions<PieShopDbContext>>()))
{
    context.Database.EnsureDeleted();
    context.Database.EnsureCreated();
}

app.Run();

public partial class Program { }