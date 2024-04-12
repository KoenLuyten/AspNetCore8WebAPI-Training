using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Moq;
using PieShopApi.Controllers;
using PieShopApi.Models.Pies;
using PieShopApi.Persistence;

namespace PieShopApiTests.Controllers
{
    public class PiesControllerTests
    {
        [Fact]
        public async Task GetPie_Should_ReturnOK()
        {
            //arrange
            var mockPieRepository = new Mock<IPieRepository>();
            var mockMapper = new Mock<IMapper>();

            mockPieRepository.Setup(p => p.GetByIdAsync(It.IsAny<int>())).ReturnsAsync(new Pie { Name = "pie name", Description = "pie description", Category = "pie category" });

            var pieDto = new PieDto { Name = "pie name", Description = "pie description" };
            mockMapper.Setup(m => m.Map<PieDto>(It.IsAny<Pie>())).Returns(pieDto);

            var controller = new PiesController(mockPieRepository.Object, mockMapper.Object);

            //act
            var result = await controller.GetPie(1);

            //assert
            Assert.IsType<OkObjectResult>(result.Result);

            var pieDtoResult = result.Result as OkObjectResult;
            Assert.Equal(pieDto, pieDtoResult.Value);
        }

        [Fact]
        public async Task GetPie_Should_ReturnNotFound()
        {
            //arrange
            var mockPieRepository = new Mock<IPieRepository>();

            mockPieRepository.Setup(p => p.GetByIdAsync(It.IsAny<int>())).ReturnsAsync((Pie)null);

            var controller = new PiesController(mockPieRepository.Object, null);

            //act
            var result = await controller.GetPie(1);

            //assert
            Assert.IsType<NotFoundResult>(result.Result);
        }

        [Fact]
        public async Task CreatePie_Should_ReturnCreated()
        {
            //arrange
            var mockPieRepository = new Mock<IPieRepository>();
            var mockMapper = new Mock<IMapper>();

            var pieForCreationDto = new PieForCreationDto { Name = "pie name", Description = "pie description" };
            var pie = new Pie { Name = "pie name", Description = "pie description", Category = "pie category" };
            var createdPie = new Pie { Id = 200, Name = "pie name", Description = "pie description", Category = "pie category" };
            var createdPieDto = new PieDto { Id = 200, Name = "pie name", Description = "pie description" };

            mockMapper.Setup(m => m.Map<Pie>(It.IsAny<PieForCreationDto>())).Returns(pie).Verifiable(Times.Once());
            mockPieRepository.Setup(p => p.AddAsync(It.IsAny<Pie>())).ReturnsAsync(createdPie).Verifiable(Times.Once());
            mockMapper.Setup(m => m.Map<PieDto>(It.IsAny<Pie>())).Returns(createdPieDto).Verifiable(Times.Once());

            var controller = new PiesController(mockPieRepository.Object, mockMapper.Object);

            //act
            var result = await controller.CreatePie(pieForCreationDto);

            //assert
            Assert.IsType<CreatedAtActionResult>(result.Result);

            var pieDtoResult = result.Result as CreatedAtActionResult;
            Assert.Equal(createdPieDto, pieDtoResult.Value);

            mockMapper.Verify();
            mockPieRepository.Verify();
        }
    }
}
