using Microsoft.AspNetCore.Mvc;

namespace PieShopApi.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class DateTimeController : ControllerBase
    {
        [HttpGet]
        [Route("fromresponsecache")]
        //[ResponseCache(Duration = 120, Location = ResponseCacheLocation.Any)]
        //[ResponseCache(Duration = 120, Location = ResponseCacheLocation.Any, VaryByHeader = "User-Agent")]
        [ResponseCache(CacheProfileName = "Cache2Minutes")]
        public IActionResult Get()
        {
            return Ok(new { DateTime = System.DateTime.Now });
        }

        [HttpGet]
        [Route("fromresponsecachebyid")]
        //[ResponseCache(Duration = 120, Location = ResponseCacheLocation.Any)]
        [ResponseCache(Duration = 120, Location = ResponseCacheLocation.Any, VaryByQueryKeys = new string[] { "id" })]
        public IActionResult Get(int id, string user)
        {
            return Ok($"{user}: response was generated for Id:{id} at {DateTime.Now}");
        }
    }
}
