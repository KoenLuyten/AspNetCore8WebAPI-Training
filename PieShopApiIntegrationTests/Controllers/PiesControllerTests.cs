using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;

namespace PieShopApiIntegrationTests.Controllers
{
    public class PiesControllerTests : IClassFixture<WebApplicationFactory<Program>>
    {
        WebApplicationFactory<Program> _factory;
        HttpClient _client;

        public PiesControllerTests()
        {
            _factory = new WebApplicationFactory<Program>();
            _client = _factory.WithWebHostBuilder(builder =>
            {
                builder.ConfigureTestServices(services =>
                {
                    // replace services for testing purposes                        
                });
            }).CreateClient(new WebApplicationFactoryClientOptions
            {
                AllowAutoRedirect = false
            });
        }

        [Fact]
        public async Task GetPies_Should_ReturnOk()
        {
            var reponse = await _client.GetAsync("/Pies");

            reponse.EnsureSuccessStatusCode();
        }
    }
}