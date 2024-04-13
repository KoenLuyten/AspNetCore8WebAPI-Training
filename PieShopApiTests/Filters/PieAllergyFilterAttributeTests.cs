using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;
using Moq;
using PieShopApi.Filters;
using PieShopApi.Models.Pies;

namespace PieShopApiTests.Filters
{
    public class PieAllergyFilterAttributeTests
    {
        [Fact]
        public async Task OnResultExecutionAsync_Should_AddNoInfoAvailable_When_PieDtoHasNoAllergyItems()
        {
            //arrange
            var pieDto = new PieDto { Name = "pie name", Description = "pie description", AllergyItems = new List<string>() };

            var httpContext = new DefaultHttpContext();
            var actionContext = new ActionContext(httpContext, new(), new(), new());
            var resultExecutingContext = new ResultExecutingContext(actionContext,
                new List<IFilterMetadata>(),
                new ObjectResult(pieDto),
                controller: null
                );
            var mockResultExecutionDelegate = new Mock<ResultExecutionDelegate>();

            var filter = new PieAllergyFilterAttribute();

            //act
            await filter.OnResultExecutionAsync(resultExecutingContext, mockResultExecutionDelegate.Object);

            //assert
            Assert.Single(pieDto.AllergyItems);
            Assert.Equal("No info available", pieDto.AllergyItems.First());
        }

        [Fact]
        public async Task OnResultExecutionAsync_Should_NotAddNoInfoAvailable_When_PieDtoHasAllergyItems()
        {
            //arrange
            var pieDto = new PieDto { Name = "pie name", Description = "pie description", AllergyItems = new List<string> { "allergy item" } };

            var httpContext = new DefaultHttpContext();
            var actionContext = new ActionContext(httpContext, new(), new(), new());
            var resultExecutingContext = new ResultExecutingContext(actionContext,
                               new List<IFilterMetadata>(),
                                              new ObjectResult(pieDto),
                                                             controller: null
                                                                            );
            var mockResultExecutionDelegate = new Mock<ResultExecutionDelegate>();

            var filter = new PieAllergyFilterAttribute();

            //act
            await filter.OnResultExecutionAsync(resultExecutingContext, mockResultExecutionDelegate.Object);

            //assert
            Assert.DoesNotContain("No info available", pieDto.AllergyItems);
            Assert.Single(pieDto.AllergyItems);
        }

        [Fact]

        public async Task OnResultExecutionAsync_Should_NotAddInfoForOtherTypes()
        {
            //arrange
            var otherType = new { foo = "bar" };

            var httpContext = new DefaultHttpContext();
            var actionContext = new ActionContext(httpContext, new(), new(), new());
            var resultExecutingContext = new ResultExecutingContext(actionContext,
                                              new List<IFilterMetadata>(),
                                              new ObjectResult(otherType),
                                                                                                                                                       controller: null
                                                                                                                                                                                                                                  );
            var mockResultExecutionDelegate = new Mock<ResultExecutionDelegate>();

            var filter = new PieAllergyFilterAttribute();

            //act
            await filter.OnResultExecutionAsync(resultExecutingContext, mockResultExecutionDelegate.Object);

            //assert
            //assert no exception was thrown
            Assert.True(true);
        }
    }
}
