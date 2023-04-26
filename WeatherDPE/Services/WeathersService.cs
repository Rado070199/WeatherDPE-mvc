using Microsoft.EntityFrameworkCore;
using WeatherDPE.Data;
using WeatherDPE.Models;

namespace WeatherDPE.Services
{
    public class WeathersService : IWeathersService
    {
        private readonly AppDbContext _context;
        public WeathersService(AppDbContext context)
        {
            _context = context;
        }

        public async Task AddAsync(WeatherData waterdata)
        {
            // pobranie ostatniego zapisanego obiektu
            var lastWeatherData = await _context.Weathers.OrderByDescending(w => w.Id).FirstOrDefaultAsync();

            // jeśli nie ma zapisanych żadnych danych lub ostatni zapisany czas jest wcześniejszy niż teraz minus jedna godzina
            if (lastWeatherData == null || lastWeatherData.Date.Date < DateTime.Now.Date || lastWeatherData.Date.Hour != DateTime.Now.Hour)
            {
                await _context.Weathers.AddAsync(waterdata);
                await _context.SaveChangesAsync();
            }


        }
        public async Task<WeatherData> GetByDateAsync(DateTime date)
        {
            return await _context.Weathers
                .Where(w => w.Date == date)
                .OrderByDescending(w => w.Id)
                .FirstOrDefaultAsync();
        }
        public async Task<WeatherData> GetLatestAsync()
        {
            return await _context.Weathers.OrderByDescending(w => w.Id).FirstOrDefaultAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var result = await _context.Weathers.FirstOrDefaultAsync(n => n.Id == id);
            _context.Weathers.Remove(result);
            await _context.SaveChangesAsync();
        }
        public async Task<IEnumerable<WeatherData>> GetAllAsync()
        {
            var result = await _context.Weathers.ToListAsync();
            return result;
        }

        public async Task<WeatherData> GetByIdAsync(int id)
        {
            var result = await _context.Weathers.FirstOrDefaultAsync(n => n.Id == id);
            return result;
        }
    }
}
