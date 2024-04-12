using Microsoft.AspNetCore.Mvc.Filters;
using PieShopApi.Persistence;

namespace PieShopApi.Filters
{
    public class AuditFilter : IAsyncActionFilter
    {
        private readonly PieShopDbContext _dbContext;

        public AuditFilter(PieShopDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            await next();

            var body = System.Text.Json.JsonSerializer.Serialize(context.ActionArguments);

            var auditEntry = new AuditEntry($"{context.HttpContext.Request.Method} - {context.HttpContext.Request.Path} - {body}");

            await _dbContext.AuditEntries.AddAsync(auditEntry);
            await _dbContext.SaveChangesAsync();
        }
    }
}
