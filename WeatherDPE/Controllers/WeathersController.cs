using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Data.Entity;
using System.Diagnostics;
using System.Net;
using System.Xml.Linq;
using WeatherDPE.Data;
using WeatherDPE.Models;
using WeatherDPE.Services;

namespace WeatherDPE.Controllers
{
    public class WeathersController : Controller
    {
        private readonly IWeathersService _service;

        public WeathersController(IWeathersService service)
        {
            _service = service;
        }

        public IActionResult Index()
        {
            string apiKey = "d8f0585898c692c1e91dfcdcd7614c70";
            double lat = 52.4069;
            double lon = 16.9299;
            WeatherData weatherData = null;

            using (WebClient web = new WebClient())
            {
                string url = string.Format("https://api.openweathermap.org/data/2.5/weather?lat={0}&lon={1}&appid={2}", lat, lon, apiKey);

                var json = web.DownloadString(url);

                dynamic jsonObj = JsonConvert.DeserializeObject(json);

                decimal tempInCelsius = Math.Round((decimal)(jsonObj.main.temp - 273.15), 1);

                weatherData = new WeatherData()
                {
                    Date = DateTime.Now,
                    Temperature = (double)tempInCelsius,
                    Pressure = jsonObj.main.pressure,
                    WindSpeed = jsonObj.wind.speed,
                    Icon = jsonObj.weather[0].icon
                };

                // dodanie do bazy danych tylko jeśli nie ma rekordu o takiej dacie i godzinie
                var existingData = _service.GetByDateAsync(weatherData.Date).Result;
                if (existingData == null)
                {
                    _service.AddAsync(weatherData).Wait();
                }
            }

            // pobranie obiektu z największym ID
            var latestWeatherData = _service.GetLatestAsync().Result;

            return View(latestWeatherData);
        }


        public async Task<IActionResult> Privacy()
        {
            var data = await _service.GetAllAsync();
            return View(data);
        }

        //Get: WeatherData/Details/1
        public async Task<IActionResult> Details(int id)
        {
            var data = await _service.GetByIdAsync(id);

            if (data == null) return View("Empty");
            return View(data);
        }

        //Get: WeatherData/Delete/1
        public async Task<IActionResult> Delete(int id)
        {
            var data = await _service.GetByIdAsync(id);
            if (data == null) return View("NotFound");
            return View(data);
        }

        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var data = await _service.GetByIdAsync(id);
            if (data == null) return View("NotFound");

            await _service.DeleteAsync(id);
            return RedirectToAction(nameof(Index));
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
