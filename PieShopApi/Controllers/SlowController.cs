using Microsoft.AspNetCore.Mvc;

namespace PieShopApi.Controllers
{
    [ApiController]
    [Route("slow")]
    public class SlowController : ControllerBase
    {
        ILogger<SlowController> _logger;

        public SlowController(ILogger<SlowController> logger)
        {
            _logger = logger;
        }

        //[HttpGet]
        //public async Task<ActionResult<string>> Get()
        //{
        //    _logger.LogInformation("Start slow request");

        //    await Task.Delay(10_000);

        //    _logger.LogInformation("End slow request");
        //    return "slow";
        //}

        [HttpGet]
        public async Task<ActionResult<string>> Get(CancellationToken cancellationToken)
        {
            _logger.LogInformation("Start slow request");

            await Task.Delay(10_000, cancellationToken);

            _logger.LogInformation("End slow request");
            return "slow";
        }

        //[HttpGet]
        //public async Task<ActionResult<string>> Get_TaskRun(CancellationToken cancellationToken)
        //{
        //    var result = await Task.Run(async () =>
        //    {
        //        await Task.Delay(10_000, cancellationToken);
        //        return "meh";
        //    });

        //    return result;
        //}

        //[HttpGet]
        //public ActionResult<string> Get_Blocking(CancellationToken cancellationToken)
        //{
        //    var result = Task.Run(async () =>
        //    {
        //        await Task.Delay(10_000, cancellationToken);
        //        return "meh";
        //    }).Result;

        //    return result;
        //}
    }
}
