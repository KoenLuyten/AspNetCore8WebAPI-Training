using Microsoft.EntityFrameworkCore;
using PieShopApi.Models.Allergies;
using PieShopApi.Models.Pies;

namespace PieShopApi.Persistence
{
    public class PieRepository : RepositoryBase<Pie>, IPieRepository
    {
        public PieRepository(PieShopDbContext dbContext) : base(dbContext)
        {
        }

        public async override Task<Pie?> GetByIdAsync(int id)
        {
            Pie? pie = await _dbContext.Pies.Include(p => p.AllergyItems)
                                            .FirstOrDefaultAsync(p => p.Id == id);

            return pie;
        }

        public async Task<Pie?> GetByPartialNameAsync(string name)
        {
            Pie? pie = await _dbContext.Pies.Where(p => p.Name.Contains(name)).FirstOrDefaultAsync();

            return pie;
        }

        public async Task<Pie?> AddAllergyAsync(Pie pie, Allergy allergy)
        {
            pie.AllergyItems.Add(allergy);
            await _dbContext.SaveChangesAsync();

            return pie;
        }

        public async Task<Pie?> RemoveAllergyAsync(Pie pie, Allergy allergy)
        {
            pie.AllergyItems.Remove(allergy);
            await _dbContext.SaveChangesAsync();

            return pie;
        }
    }

    public interface IPieRepository : IAsyncRepository<Pie>
    {
        Task<Pie?> GetByPartialNameAsync(string name);
        Task<Pie?> AddAllergyAsync(Pie pie, Allergy allergy);
        Task<Pie?> RemoveAllergyAsync(Pie pie, Allergy allergy);
    }
}
