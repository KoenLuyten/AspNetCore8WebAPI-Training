//using AutoMapper;
//using Microsoft.AspNetCore.Mvc;
//using PieShopApi.Filters;
//using PieShopApi.Models.Pies;
//using PieShopApi.Persistence;
//using System.Xml.Linq;

//namespace PieShopApi.Controllers
//{
//    [ApiController]
//    [Route("pies")]
//    //[LoggingFilter]
//    public class PiesController : ControllerBase
//    {
//        private readonly IPieRepository _pieRepository;
//        private readonly IMapper _mapper;

//        public PiesController(IPieRepository pieRepository, IMapper mapper)
//        {
//            _pieRepository = pieRepository;
//            _mapper = mapper;
//        }

//        [HttpGet]
//        //[LoggingFilter]
//        public async Task<ActionResult<IEnumerable<PieForListDto>>> GetPies([FromQuery] PieListParameters parameters)
//        {
//            var pieList = await _pieRepository.ListPiesAsync(parameters.Category, 
//                                                             parameters.SearchTerm, 
//                                                             parameters.PageNumber, 
//                                                             parameters.PageSize);

//            var metadata = new
//            {
//                pieList.TotalCount,
//                pieList.PageSize,
//                pieList.CurrentPage,
//                pieList.TotalPages,
//                pieList.HasNext,
//                pieList.HasPrevious
//            };

//            Response.Headers.Append("X-Pagination", System.Text.Json.JsonSerializer.Serialize(metadata));

//            return Ok(_mapper.Map<IEnumerable<PieForListDto>>(pieList));
//        }

//        [HttpGet]
//        [PieAllergyFilter]
//        [Route("{id:int}", Name = "GetPie")]
//        public async Task<ActionResult<PieDto>> GetPie(int id)
//        {
//            var pie = await _pieRepository.GetByIdAsync(id);

//            if (pie == null)
//                return NotFound();

//            var pieDto = _mapper.Map<PieDto>(pie);

//            return Ok(pieDto);
//        }

//        [HttpGet]
//        [Route("search")]
//        public async Task<ActionResult<Pie>> SearchPie(string name)
//        {
//            var pie = await _pieRepository.GetByPartialNameAsync(name);

//            if (pie == null)
//                return NotFound();

//            return Ok(pie);
//        }

//        [HttpGet]
//        [Route("filter")]
//        public async Task<ActionResult<Pie>> FilterPie()
//        {
//            throw new NotImplementedException();
//        }

//        [HttpPost]
//        public async Task<ActionResult<Pie>> CreatePie(PieForCreationDto pie)
//        {
//            var pieToAdd = _mapper.Map<Pie>(pie);

//            var createdPie = await _pieRepository.AddAsync(pieToAdd);

//            return CreatedAtAction(nameof(GetPie), new { id = createdPie.Id }, _mapper.Map<PieDto>(createdPie));
//        }

//        [HttpPut]
//        [Route("{id:int}")]
//        public async Task<ActionResult<Pie>> UpdatePie(int id, PieForUpdateDto pie)
//        {
//            var currentPie = await _pieRepository.GetByIdAsync(id);

//            if (currentPie == null)
//                return NotFound();

//            _mapper.Map(pie, currentPie);

//            await _pieRepository.UpdateAsync(currentPie);

//            return NoContent();
//        }

//        [HttpDelete]
//        [Route("{id:int}")]
//        public async Task<ActionResult> DeletePie(int id)
//        {
//            var pie = await _pieRepository.GetByIdAsync(id);
//            if (pie == null)
//            {
//                return NotFound();
//            }

//            await _pieRepository.DeleteAsync(pie);

//            return NoContent();
//        }
//    }

//    //public class PieStore
//    //{
//    //    public static IEnumerable<Pie> GetAll()
//    //    {
//    //        return new List<Pie>
//    //        {
//    //            new Pie { Id = 1, Name = "Apple Pie", Description = "Tasty" },
//    //            new Pie { Id = 2, Name = "Cherry Pie", Description = "Yummy" },
//    //            new Pie { Id = 3, Name = "Pumpkin Pie", Description = "Delicious" }
//    //        };
//    //    }

//    //    public static Pie GetById(int id)
//    //    {
//    //        return GetAll().SingleOrDefault(p => p.Id == id);
//    //    }

//    //    public static Pie GetByPartialName(string name)
//    //    {
//    //        return GetAll().FirstOrDefault(p => p.Name.ToLowerInvariant().Contains(name.ToLowerInvariant()));
//    //    }
//    //}
//}
