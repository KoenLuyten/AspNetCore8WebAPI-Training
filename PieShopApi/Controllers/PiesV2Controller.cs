using Asp.Versioning;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using PieShopApi.Models.Pies;
using PieShopApi.Persistence;

namespace PieShopApi.Controllers
{
    [ApiController]
    [ApiVersion("2.0")]
    [Route("v{version:apiVersion}/pies")]
    //[LoggingFilter]
    public class PiesV2Controller : ControllerBase
    {
        private readonly IPieRepository _pieRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<PiesController> _logger;

        public PiesV2Controller(IPieRepository pieRepository, IMapper mapper, ILogger<PiesController> logger)
        {
            _pieRepository = pieRepository;
            _mapper = mapper;
            _logger = logger;
        }

        /// <summary>
        /// Gets a paged list of pies
        /// </summary>
        /// <remarks>
        /// Sample request:
        /// 
        ///     GET /pies?category=fruit&amp;search=apple&amp;pageNumber=1&amp;pageSize=10
        /// </remarks>
        /// <param name="parameters">the filter, search and paging parameters</param>
        /// <returns>Paged List of Pies</returns>
        [HttpGet]
        //[LoggingFilter]
        [Produces("application/json", "application/xml")]
        public async Task<ActionResult<IEnumerable<PieForListDto>>> GetPies([FromQuery] PieListParameters parameters)
        {
            var pieList = await _pieRepository.ListPiesAsync(parameters.Category,
                                                             parameters.SearchTerm,
                                                             parameters.PageNumber,
                                                             parameters.PageSize);

            var metadata = new
            {
                pieList.TotalCount,
                pieList.PageSize,
                pieList.CurrentPage,
                pieList.TotalPages,
                pieList.HasNext,
                pieList.HasPrevious
            };

            Response.Headers.Append("X-Pagination", System.Text.Json.JsonSerializer.Serialize(metadata));

            return Ok(_mapper.Map<IEnumerable<PieForListDto>>(pieList));
        }
    }
}
