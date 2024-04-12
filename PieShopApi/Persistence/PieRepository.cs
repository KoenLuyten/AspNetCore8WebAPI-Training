using Microsoft.EntityFrameworkCore;
using PieShopApi.Models;

namespace PieShopApi.Persistence
{
    public class PieRepository : RepositoryBase<Pie>, IPieRepository
    {
        public PieRepository(PieShopDbContext dbContext) : base(dbContext)
        {
        }

        public async Task<Pie?> GetByPartialNameAsync(string name)
        {
            Pie? pie = await _dbContext.Pies.Where(p => p.Name.Contains(name)).FirstOrDefaultAsync();

            return pie;
        }
    }

    public interface IPieRepository : IAsyncRepository<Pie>
    {
        Task<Pie?> GetByPartialNameAsync(string name);
    }
}
