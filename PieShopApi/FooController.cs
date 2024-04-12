using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace PieShopApi
{
    [ApiController]
    [Route("foo")]
    public class FooController: ControllerBase
    {
        [HttpGet, HttpPost]
        public ActionResult Get()
        {
            var bar = Request.Query["bar"];

            Response.Headers.Append("X-Foo", "Bar");

            return Ok(bar);
        }

        [HttpPut]
        public ActionResult Put(FooModel model)
        {
            //if (!ModelState.IsValid)
            //{
            //    return BadRequest(ModelState);
            //}

            return Ok(model);
        }
    }

    public class FooModel
    {
        [Required]
        public string Name { get; set; }
    }
}
