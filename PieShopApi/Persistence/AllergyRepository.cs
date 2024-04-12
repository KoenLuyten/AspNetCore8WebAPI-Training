using PieShopApi.Models.Allergies;

namespace PieShopApi.Persistence
{
    public class AllergyRepository : RepositoryBase<Allergy>, IAllergyRepository
    {
        public AllergyRepository(PieShopDbContext dbContext) : base(dbContext)
        {
        }
    }

    public interface IAllergyRepository : IAsyncRepository<Allergy>
    {
    }
}
