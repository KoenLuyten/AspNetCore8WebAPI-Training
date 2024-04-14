using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OutputCaching;

namespace PieShopApi.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class DateTimeController : ControllerBase
    {
        private readonly IOutputCacheStore _cachStore;

        public DateTimeController(IOutputCacheStore cachStore)
        {
            _cachStore = cachStore;
        }

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

        [HttpGet]
        [Route("fromoutputcache")]
        //[OutputCache]
        //[OutputCache(Duration = 120, VaryByHeaderNames = new string[] { "X-CacheKey" })]
        [OutputCache(PolicyName = "CacheForThirtySeconds")]
        public async Task<IActionResult> GetOutputCache()
        {
            await Task.Delay(5000);

            return Ok(new { DateTime = System.DateTime.Now });
        }

        [HttpGet]
        [Route("fromoutputcacherevalidation")]
        [OutputCache]
        public async Task<IActionResult> GetOutputCacheRevalidaiton()
        {
            var etag = $"\"{Guid.NewGuid():n}\"";
            Response.Headers.ETag = etag;

            await Task.Delay(5000);

            return Ok(new { DateTime = System.DateTime.Now });
        }

        [HttpGet]
        [Route("fromoutputcacheeviction")]
        [OutputCache(Tags = new string[] { "tag-datetime" })]
        public async Task<IActionResult> GetOutputCacheEviction()
        {
            await Task.Delay(5000);

            return Ok(new { DateTime = System.DateTime.Now });
        }

        [HttpPost]
        [Route("purge/{tag}")]
        public async Task<IActionResult> Purge(string tag)
        {
            await _cachStore.EvictByTagAsync(tag, default);

            return NoContent();
        }
    }
}
