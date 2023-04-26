using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WeatherDPE.Models;

namespace WeatherDPE.Tests.Models
{
    public class WeatherDataTests
    {
        [Fact]
        public void CanCreateWeatherData()
        {
            // Arrange
            DateTime date = new DateTime(2023, 04, 26, 12, 0, 0);
            double temperature = 20.5;
            double pressure = 1013.25;
            double windSpeed = 5.0;
            string icon = "01d";

            // Act
            var weatherData = new WeatherData
            {
                Date = date,
                Temperature = temperature,
                Pressure = pressure,
                WindSpeed = windSpeed,
                Icon = icon
            };

            // Assert
            Assert.Equal(date, weatherData.Date);
            Assert.Equal(temperature, weatherData.Temperature);
            Assert.Equal(pressure, weatherData.Pressure);
            Assert.Equal(windSpeed, weatherData.WindSpeed);
            Assert.Equal(icon, weatherData.Icon);
        }
    }
}
