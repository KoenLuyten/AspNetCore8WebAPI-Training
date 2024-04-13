using AutoMapper;
using PieShopApi.Controllers;

namespace PieShopApiTests.Mappers
{
    public class AutoMapperConfigurationTests
    {
        [Fact]
        public void AutoMapper_Configuration_IsValid()
        {
            // scan for all profiles in the assembly and test if they are valid
            var configuration = new MapperConfiguration(cfg => cfg.AddMaps(typeof(PiesController).Assembly));

            configuration.AssertConfigurationIsValid();
        }
    }
}
