using Microsoft.EntityFrameworkCore;
using MinimalPieShopApi.Models;

namespace MinimalPieShopApi.Persistence
{
    public class PieRepository : RepositoryBase<Pie>, IPieRepository
    {
        public PieRepository(PieShopDbContext dbContext) : base(dbContext)
        {
        }

        public async override Task<Pie?> GetByIdAsync(int id)
        {
            Pie? pie = await _dbContext.Pies.FirstOrDefaultAsync(p => p.Id == id);

            return pie;
        }

        public async Task<PagedList<Pie>> ListPiesAsync(string? category, string? searchTerm, PieListParameters parameters)
        {
            var pies = _dbContext.Pies.AsQueryable();

            if (!string.IsNullOrWhiteSpace(category))
            {
                category = category.ToLower().Trim();

                pies = pies.Where(p => p.Category.ToLower() == category);
            }

            if (!string.IsNullOrWhiteSpace(searchTerm))
            {
                searchTerm = searchTerm.ToLower().Trim();

                pies = pies.Where(p => p.Name.ToLower().Contains(searchTerm)
                            || p.Description.ToLower().Contains(searchTerm)
                            || p.Category.ToLower().Contains(searchTerm));
            }

            return await PagedList<Pie>.ToPagedList(pies, parameters.PageNumber, parameters.PageSize);
        }
    }

    public interface IPieRepository : IAsyncRepository<Pie>
    {
        Task<PagedList<Pie>> ListPiesAsync(string? category, string? searchTerm, PieListParameters paramaters);
    }
}
