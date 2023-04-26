using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System;
using System.Threading.Tasks;
using WeatherDPE.Controllers;
using WeatherDPE.Models;
using WeatherDPE.Services;
using Xunit;

namespace WeatherDPE.Tests
{
    public class WeathersControllerTests
    {
        private readonly Mock<IWeathersService> _weatherServiceMock;
        private readonly WeathersController _controller;

        public WeathersControllerTests()
        {
            _weatherServiceMock = new Mock<IWeathersService>();
            _controller = new WeathersController(_weatherServiceMock.Object);
        }

        [Fact]
        public async Task Index_ReturnsViewWithLatestWeatherData()
        {
            // Arrange
            var expectedWeatherData = new WeatherData { Id = 1, Date = DateTime.Now, Temperature = 15.0, Pressure = 1013.0, WindSpeed = 5.0, Icon = "01d" };
            _weatherServiceMock.Setup(x => x.GetLatestAsync()).ReturnsAsync(expectedWeatherData);

            // Act
            var result = _controller.Index();

            // Assert
            var viewResult = Assert.IsType<ViewResult>(result);
            var model = Assert.IsAssignableFrom<WeatherData>(viewResult.ViewData.Model);
            Assert.Equal(expectedWeatherData, model);
        }

        [Fact]
        public async Task Create_ReturnsRedirectToActionResult()
        {
            // Arrange
            var expectedWeatherData = new WeatherData { Id = 1, Date = DateTime.Now, Temperature = 15.0, Pressure = 1013.0, WindSpeed = 5.0, Icon = "01d" };
            _weatherServiceMock.Setup(x => x.GetByDateAsync(It.IsAny<DateTime>())).ReturnsAsync((WeatherData)null);
            _weatherServiceMock.Setup(x => x.AddAsync(It.IsAny<WeatherData>())).Returns(Task.CompletedTask);

            // Act
            var result = await _controller.Create();

            // Assert
            var redirectToActionResult = Assert.IsType<RedirectToActionResult>(result);
            Assert.Equal("Index", redirectToActionResult.ActionName);
        }

        [Fact]
        public async Task Privacy_ReturnsViewWithListOfWeatherData()
        {
            // Arrange
            var expectedWeatherDataList = new[] { new WeatherData { Id = 1, Date = DateTime.Now, Temperature = 15.0, Pressure = 1013.0, WindSpeed = 5.0, Icon = "01d" } };
            _weatherServiceMock.Setup(x => x.GetAllAsync()).ReturnsAsync(expectedWeatherDataList);

            // Act
            var result = await _controller.Privacy();

            // Assert
            var viewResult = Assert.IsType<ViewResult>(result);
            var model = Assert.IsAssignableFrom<WeatherData[]>(viewResult.ViewData.Model);
            Assert.Equal(expectedWeatherDataList, model);
        }

        [Fact]
        public async Task Details_ReturnsViewWithWeatherData()
        {
            // Arrange
            var expectedWeatherData = new WeatherData { Id = 1, Date = DateTime.Now, Temperature = 15.0, Pressure = 1013.0, WindSpeed = 5.0, Icon = "01d" };
            _weatherServiceMock.Setup(x => x.GetByIdAsync(It.IsAny<int>())).ReturnsAsync(expectedWeatherData);

            // Act
            var result = await _controller.Details(1);

            // Assert
            var viewResult = Assert.IsType<ViewResult>(result);
            var model = Assert.IsAssignableFrom<WeatherData>(viewResult.ViewData.Model);
            Assert.Equal(expectedWeatherData, model);
            _weatherServiceMock.Verify(x => x.GetByIdAsync(It.IsAny<int>()), Times.Once);
        }
        [Fact]
        public async Task Delete_ReturnsNotFoundView_WhenWeatherDataIsNull()
        {
            // Arrange
            int id = 1;
            _weatherServiceMock.Setup(x => x.GetByIdAsync(id)).ReturnsAsync((WeatherData)null);

            // Act
            var result = await _controller.Delete(id);

            // Assert
            var viewResult = Assert.IsType<ViewResult>(result);
            Assert.Equal("NotFound", viewResult.ViewName);
        }

        [Fact]
        public async Task Delete_ReturnsViewWithWeatherData()
        {
            // Arrange
            int id = 1;
            var expectedWeatherData = new WeatherData { Id = id, Date = DateTime.Now, Temperature = 15.0, Pressure = 1013.0, WindSpeed = 5.0, Icon = "01d" };
            _weatherServiceMock.Setup(x => x.GetByIdAsync(id)).ReturnsAsync(expectedWeatherData);

            // Act
            var result = await _controller.Delete(id);

            // Assert
            var viewResult = Assert.IsType<ViewResult>(result);
            var model = Assert.IsAssignableFrom<WeatherData>(viewResult.ViewData.Model);
            Assert.Equal(expectedWeatherData, model);
        }
    }
}
