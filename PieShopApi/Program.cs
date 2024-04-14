using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.RateLimiting;
using Microsoft.AspNetCore.StaticFiles;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using PieShopApi.Filters;
using PieShopApi.Formatters;
using PieShopApi.Persistence;
using System.Reflection;
using System.Threading.RateLimiting;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<PieShopDbContext>(options =>
    options.UseInMemoryDatabase("PieShopDb")
           .LogTo(Console.WriteLine));

builder.Services.AddScoped(typeof(IPieRepository), typeof(PieRepository));
builder.Services.AddScoped(typeof(IAllergyRepository), typeof(AllergyRepository));

builder.Services.AddSingleton(new FileExtensionContentTypeProvider());

builder.Services.AddAutoMapper(Assembly.GetExecutingAssembly());

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", builder => builder.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());

    options.AddPolicy("AllowLocalhost8080", builder => builder.WithOrigins("https://localhost:8080")
                                                              .AllowAnyMethod()
                                                              .AllowAnyHeader());

    // 7282 is the port number of the Blazor WebAssembly app
    options.AddPolicy("AllowLocalhost7282", builder => builder.WithOrigins("https://localhost:7282")
                                                              .AllowAnyMethod()
                                                              .AllowAnyHeader());

});

builder.Services.AddResponseCaching();

builder.Services.AddRateLimiter(_ => _
    .AddFixedWindowLimiter(policyName: "myWindowLimiter", options =>
    {
        options.PermitLimit = 4;
        options.Window = TimeSpan.FromSeconds(60);
        options.QueueProcessingOrder = QueueProcessingOrder.OldestFirst;
        options.QueueLimit = 2;
    })
    .RejectionStatusCode = 429);

builder.Services.AddControllers((options) =>
{
    options.Filters.Add<LoggingFilterAttribute>();
    options.CacheProfiles.Add("Cache2Minutes", new CacheProfile
    {
        Duration = 120,
        Location = ResponseCacheLocation.Any
    });
    options.RespectBrowserAcceptHeader = true;
    options.ReturnHttpNotAcceptable = true;
    options.OutputFormatters.Add(new PieCsvFormatter());
}).AddXmlDataContractSerializerFormatters();

//builder.Services.AddOutputCache();

builder.Services.AddOutputCache(options =>
{
    //options.AddBasePolicy(builder =>
    //    builder.Expire(TimeSpan.FromSeconds(10)));
    options.AddPolicy("CacheForThirtySeconds", builder =>
        builder.Expire(TimeSpan.FromSeconds(30)));
});

builder.Services.AddProblemDetails(
    options =>
        options.CustomizeProblemDetails = ctx =>
        {
            ctx.ProblemDetails.Extensions.Add("version", Assembly.GetExecutingAssembly().GetName().Version);
        }
);
builder.Services.AddApplicationInsightsTelemetry();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "PieShop",
        Version = "v1 version",
        Description = "Api to get the pies",
        TermsOfService = new Uri("https://xebia.com/terms"),
        Contact = new OpenApiContact
        {
            Name = "John Doe",
            Email = "John.Doe@xebia.com",
            Url = new Uri("https://www.xebia.com"),
        },
        License = new OpenApiLicense
        {
            Name = "PieShop License",
            Url = new Uri("https://xebia.com/license"),
        }
    });
});

var app = builder.Build();

//app.UseCors("AllowLocalhost8080");
//app.UseCors("AllowLocalhost7282");
app.UseCors("AllowAll");

app.UseResponseCaching();

app.UseRateLimiter();

app.UseSwagger();
app.UseSwaggerUI();

app.MapControllers();

app.UseOutputCache();

if (!app.Environment.IsDevelopment())
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