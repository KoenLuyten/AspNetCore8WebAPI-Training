using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;
using PieShopApi.Models.Pies;

namespace PieShopApi.Filters
{
    public class PieAllergyFilterAttribute : ResultFilterAttribute
    {
        public override async Task OnResultExecutionAsync(ResultExecutingContext context, ResultExecutionDelegate next)
        {
            if (context.Result is ObjectResult objectResult)
            {
                if (objectResult.Value is PieDto pieDto)
                {
                    if (pieDto.AllergyItems.Count == 0)
                    {
                        pieDto.AllergyItems.Add("No info available");
                    }
                }
            }

            await next();
        }
    }
}
