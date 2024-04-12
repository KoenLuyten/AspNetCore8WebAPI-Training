using Microsoft.AspNetCore.Mvc.Filters;
using System.Diagnostics;

namespace PieShopApi.Filters
{
    public class LoggingFilterAttribute : ActionFilterAttribute
    {
        public override async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            //get current url
            var url = context.HttpContext.Request.Path.Value;
            Console.WriteLine($">>>>> Start excuting action on route {url}");
            var stopwatch = Stopwatch.StartNew();

            await next();

            stopwatch.Stop();
            Console.WriteLine($">>>>> Finished excuting action on route {url} in {stopwatch.ElapsedMilliseconds}ms");
        }
    }
}
