using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Query;
using Microsoft.AspNetCore.OData.Routing.Controllers;
using PieShopApi.Models.Pies;
using PieShopApi.Persistence;

namespace PieShopApi.ODataControllers
{
    public class PiesController : ODataController
    {
        private readonly PieShopDbContext _dbContext;

        public PiesController(PieShopDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        [EnableQuery(PageSize = 20)]

        public ActionResult<IQueryable<Pie>> Get()
        {
            return Ok(_dbContext.Pies);
        }

        [EnableQuery]
        public ActionResult<Pie> Get([FromRoute] int key)
        {
            var customer = _dbContext.Pies.SingleOrDefault(d => d.Id == key);

            if (customer == null)
            {
                return NotFound();
            }

            return Ok(customer);
        }
    }
}
