using Microsoft.AspNetCore.Mvc;
using PieShopApi.Models;

namespace PieShopApi.Controllers
{
    [ApiController]
    [Route("pies")]
    public class PiesController: ControllerBase
    {
        [HttpGet]
        public JsonResult GetPies()
        {
            var pies = new List<Pie>
            {
                new Pie { Id = 1, Name = "Apple Pie", Description = "Our famous apple pies!" },
                new Pie { Id = 2, Name = "Blueberry Pie", Description = "You'll love our blueberry pies!" },
                new Pie { Id = 3, Name = "Cherry Pie", Description = "Our cherry pies are the best!" }
            };
            
            return new JsonResult(pies);
        }
    }
}
