using AutoMapper;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using MinimalPieShopApi.Models;
using MinimalPieShopApi.Persistence;

namespace MinimalPieShopApi.Endpoints
{
    public static class PieEndpoints
    {
        public static void MapPieEndpoints(this WebApplication app)
        {
            var pieGroup = app.MapGroup("/pies/")
                  .WithTags("Pie Endpoints");

            pieGroup.MapGet("", async Task<Ok<IEnumerable<PieForListDto>>> (string? category, string? searchTerm, [AsParameters] PieListParameters pageParams, [FromServices] IPieRepository repository, [FromServices] IMapper mapper) =>
            {
                var result = await repository.ListPiesAsync(category, searchTerm, pageParams);

                return TypedResults.Ok(mapper.Map<IEnumerable<PieForListDto>>(result));
            });

            const string GetPieRouteName = "GetPie";
            pieGroup.MapGet("{id}", async Task<Results<NotFound, Ok<PieDto>>> (int id, IPieRepository repository, [FromServices] IMapper mapper) =>
            {
                var pie = await repository.GetByIdAsync(id);

                if (pie == null)
                {
                    return TypedResults.NotFound();
                }

                return TypedResults.Ok(mapper.Map<PieDto>(pie));
            }).WithName(GetPieRouteName);

            pieGroup.MapPost("", async Task<CreatedAtRoute> (PieForCreationDto pie, IPieRepository repository, [FromServices] IMapper mapper) =>
            {
                var pieToCreate = mapper.Map<Pie>(pie);

                var savedPie = await repository.AddAsync(pieToCreate);

                var pieDto = mapper.Map<PieDto>(savedPie);

                return TypedResults.CreatedAtRoute(GetPieRouteName, pieDto);
            });

            pieGroup.MapPut("{id}", async Task<Results<NotFound, NoContent>> (int id, PieForUpdateDto pie, IPieRepository repository, [FromServices] IMapper mapper) =>
            {
                var existingPie = await repository.GetByIdAsync(id);
                if (existingPie == null)
                {
                    return TypedResults.NotFound();
                }

                mapper.Map(pie, existingPie);

                await repository.UpdateAsync(existingPie);

                return TypedResults.NoContent();
            });

            pieGroup.MapDelete("{id}", async (int id, IPieRepository repository) =>
            {
                var existingPie = await repository.GetByIdAsync(id);
                if (existingPie == null)
                {
                    return Results.NotFound();
                }

                await repository.DeleteAsync(existingPie);

                return Results.NoContent();
            });
        }
    }
}
