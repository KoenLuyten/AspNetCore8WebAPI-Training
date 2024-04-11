using Microsoft.AspNetCore.Mvc;
using PieShopApi.Models;

namespace PieShopApi.Controllers
{
    [ApiController]
    [Route("pies")]
    public class PiesController : ControllerBase
    {
        [HttpGet]
        public JsonResult GetPies()
        {
            return new JsonResult(PieStore.GetAll());
        }

        [HttpGet]
        [Route("{id:int}")]
        public ActionResult<Pie> GetPie(int id)
        {
            return PieStore.GetById(id);
        }

        [HttpGet]
        [Route("search")]
        public ActionResult<Pie> SearchPie(string name)
        {
            return PieStore.GetByPartialName(name);
        }
    }

    public class PieStore
    {
        public static IEnumerable<Pie> GetAll()
        {
            return new List<Pie>
            {
                new Pie { Id = 1, Name = "Apple Pie", Description = "Tasty" },
                new Pie { Id = 2, Name = "Cherry Pie", Description = "Yummy" },
                new Pie { Id = 3, Name = "Pumpkin Pie", Description = "Delicious" }
            };
        }

        public static Pie GetById(int id)
        {
            return GetAll().SingleOrDefault(p => p.Id == id);
        }

        public static Pie GetByPartialName(string name)
        {
            return GetAll().FirstOrDefault(p => p.Name.ToLowerInvariant().Contains(name.ToLowerInvariant()));
        }
    }
}
