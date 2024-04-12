using Microsoft.AspNetCore.Mvc;
using PieShopApi.Models.Pies;
using PieShopApi.Persistence;
using System.Xml.Linq;

namespace PieShopApi.Controllers
{
    [ApiController]
    [Route("pies")]
    public class PiesController : ControllerBase
    {
        private readonly IPieRepository _pieRepository;

        public PiesController(IPieRepository pieRepository)
        {
            _pieRepository = pieRepository;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<PieForListDto>>> GetPies(int? size, int? page)
        {
            if ((size.HasValue && (size.Value <= 0 || size.Value > 50)) || (page.HasValue && page.Value <= 0))
            {
                //return BadRequest(new
                //{
                //    error = "Bad input!",
                //    details = "Size, if provided, must be between 1 and 50. Page, if provided, must be greater than 0."
                //});

                return Problem(
                    title: "Bad Input",
                    detail: "Size, if provided, must be between 1 and 50. Page, if provided, must be greater than 0.",
                    type: "Paging_Error",
                    statusCode: StatusCodes.Status400BadRequest
                );
            }

            if (size.HasValue && size.Value > 0 && (page.HasValue && page > 0 || !page.HasValue))
            {
                var pieListPaged = await _pieRepository.GetPagedReponseAsync(page ?? 1, size.Value);
                return Ok(pieListPaged.Select(x => new PieForListDto { Id = x.Id, Name = x.Name, Description = x.Description }));
            }

            var pieList = await _pieRepository.ListAllAsync();

            return Ok(pieList.Select(x => new PieForListDto { Id = x.Id, Name = x.Name, Description = x.Description }));
        }

        [HttpGet]
        [Route("{id:int}")]
        public async Task<ActionResult<PieDto>> GetPie(int id)
        {
            var pie = await _pieRepository.GetByIdAsync(id);

            if (pie == null)
                return NotFound();

            var pieDto = new PieDto
            {
                Id = pie.Id,
                Name = pie.Name,
                Description = pie.Description,
                AllergyItems = pie.AllergyItems.Select(x => x.Name).ToList()
            };

            return Ok(pieDto);
        }

        [HttpGet]
        [Route("search")]
        public async Task<ActionResult<Pie>> SearchPie(string name)
        {
            var pie = await _pieRepository.GetByPartialNameAsync(name);

            if (pie == null)
                return NotFound();

            return Ok(pie);
        }

        [HttpGet]
        [Route("filter")]
        public async Task<ActionResult<Pie>> FilterPie()
        {
            throw new NotImplementedException();
        }

        [HttpPost]
        public async Task<ActionResult<Pie>> CreatePie(PieForCreationDto pie)
        {
            var pieToAdd = new Pie
            {
                Name = pie.Name,
                Description = pie.Description
            };

            var createdPie = await _pieRepository.AddAsync(pieToAdd);

            var createdPieDto = new PieDto
            {
                Id = createdPie.Id,
                Name = createdPie.Name,
                Description = createdPie.Description,
                AllergyItems = createdPie.AllergyItems.Select(x => x.Name).ToList()
            };

            return CreatedAtAction(nameof(GetPie), new { id = createdPie.Id }, createdPieDto);
        }

        [HttpPut]
        [Route("{id:int}")]
        public async Task<ActionResult<Pie>> UpdatePie(int id, PieForUpdateDto pie)
        {
            var currentPie = await _pieRepository.GetByIdAsync(id);

            if (currentPie == null)
                return NotFound();

            currentPie.Name = pie.Name;
            currentPie.Description = pie.Description;

            await _pieRepository.UpdateAsync(currentPie);

            return NoContent();
        }

        [HttpDelete]
        [Route("{id:int}")]
        public async Task<ActionResult> DeletePie(int id)
        {
            var pie = await _pieRepository.GetByIdAsync(id);
            if (pie == null)
            {
                return NotFound();
            }

            await _pieRepository.DeleteAsync(pie);

            return NoContent();
        }
    }

    //public class PieStore
    //{
    //    public static IEnumerable<Pie> GetAll()
    //    {
    //        return new List<Pie>
    //        {
    //            new Pie { Id = 1, Name = "Apple Pie", Description = "Tasty" },
    //            new Pie { Id = 2, Name = "Cherry Pie", Description = "Yummy" },
    //            new Pie { Id = 3, Name = "Pumpkin Pie", Description = "Delicious" }
    //        };
    //    }

    //    public static Pie GetById(int id)
    //    {
    //        return GetAll().SingleOrDefault(p => p.Id == id);
    //    }

    //    public static Pie GetByPartialName(string name)
    //    {
    //        return GetAll().FirstOrDefault(p => p.Name.ToLowerInvariant().Contains(name.ToLowerInvariant()));
    //    }
    //}
}
