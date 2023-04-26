using WeatherDPE.Models;

namespace WeatherDPE.Services
{
    public interface IWeathersService
    {
        Task<IEnumerable<WeatherData>> GetAllAsync();
        Task<WeatherData> GetByIdAsync(int id);
        Task AddAsync(WeatherData waterdata);
        Task<WeatherData> GetByDateAsync(DateTime date);
        Task<WeatherData> GetLatestAsync();
        Task DeleteAsync(int id);
    }
}
