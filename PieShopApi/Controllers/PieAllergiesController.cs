using Microsoft.AspNetCore.Mvc;
using PieShopApi.Models.Allergies;
using PieShopApi.Persistence;

namespace PieShopApi.Controllers
{
    [ApiController]
    [Route("pies/{pieId:int}/allergies")]
    public class PieAllergiesController: ControllerBase
    {
        private readonly IPieRepository _pieRepository;
        private readonly IAllergyRepository _allergyRepository;

        public PieAllergiesController(IPieRepository pieRepository, IAllergyRepository allergyRepository)
        {
            _pieRepository = pieRepository;
            _allergyRepository = allergyRepository;
        }

        [HttpPut]
        [Route("{allergyId:int}")]
        public async Task<IActionResult> Put(int pieId, int allergyId)
        {
            var pie = await _pieRepository.GetByIdAsync(pieId);
            var allergy = await _allergyRepository.GetByIdAsync(allergyId);

            if (pie == null || allergy == null)
            {
                return NotFound();
            }

            if (pie.AllergyItems.Contains(allergy))
            {
                return NoContent();
            }

            await _pieRepository.AddAllergyAsync(pie, allergy);

            return CreatedAtRoute("GetPie", new { id = pieId }, null);
        }

        [HttpDelete]
        [Route("{allergyId:int}")]
        public async Task<IActionResult> Delete(int pieId, int allergyId)
        {
            var pie = await _pieRepository.GetByIdAsync(pieId);
            var allergy = await _allergyRepository.GetByIdAsync(allergyId);

            if (pie == null || allergy == null)
            {
                return NotFound();
            }

            await _pieRepository.RemoveAllergyAsync(pie, allergy);

            return NoContent();
        }
    }
}
