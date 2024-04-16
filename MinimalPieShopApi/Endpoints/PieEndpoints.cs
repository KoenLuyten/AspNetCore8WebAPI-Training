using AutoMapper;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using MinimalPieShopApi.Models;
using MinimalPieShopApi.Persistence;

namespace MinimalPieShopApi.Endpoints
{
    public static class PieEndpoints
    {
        const string GetPieRouteName = "GetPie";
        public static void MapPieEndpoints(this WebApplication app)
        {
            var pieGroup = app.MapGroup("/pies/")
                  .WithTags("Pie Endpoints");

            pieGroup.MapGet("", GetPieList);

            pieGroup.MapGet("{id}", GetPieById).WithName(GetPieRouteName);

            pieGroup.MapPost("", PostPie);

            pieGroup.MapPut("{id}", PutPie);

            pieGroup.MapDelete("{id}", DeletePie);
        }

        public static async Task<Ok<IEnumerable<PieForListDto>>> GetPieList(string? category, string? searchTerm, [AsParameters] PieListParameters pageParams, [FromServices] IPieRepository repository, [FromServices] IMapper mapper)
        {
            var result = await repository.ListPiesAsync(category, searchTerm, pageParams);

            return TypedResults.Ok(mapper.Map<IEnumerable<PieForListDto>>(result));
        }

        public static async Task<Results<NotFound, Ok<PieDto>>> GetPieById(int id, IPieRepository repository, [FromServices] IMapper mapper)
        {
            var pie = await repository.GetByIdAsync(id);

            if (pie == null)
            {
                return TypedResults.NotFound();
            }

            return TypedResults.Ok(mapper.Map<PieDto>(pie));
        }

        public static async Task<CreatedAtRoute> PostPie(PieForCreationDto pie, IPieRepository repository, [FromServices] IMapper mapper)
        {
            var pieToCreate = mapper.Map<Pie>(pie);

            var savedPie = await repository.AddAsync(pieToCreate);

            var pieDto = mapper.Map<PieDto>(savedPie);

            return TypedResults.CreatedAtRoute(GetPieRouteName, pieDto);
        }

        public static async Task<Results<NotFound, NoContent>> PutPie(int id, PieForUpdateDto pie, IPieRepository repository, [FromServices] IMapper mapper)
        {
            var existingPie = await repository.GetByIdAsync(id);
            if (existingPie == null)
            {
                return TypedResults.NotFound();
            }

            mapper.Map(pie, existingPie);

            await repository.UpdateAsync(existingPie);

            return TypedResults.NoContent();
        }

        public static async Task<Results<NotFound, NoContent>> DeletePie(int id, IPieRepository repository)
        {
            var existingPie = await repository.GetByIdAsync(id);
            if (existingPie == null)
            {
                return TypedResults.NotFound();
            }

            await repository.DeleteAsync(existingPie);

            return TypedResults.NoContent();
        }
    }
}
