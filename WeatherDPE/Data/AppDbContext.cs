using Microsoft.EntityFrameworkCore;
using WeatherDPE.Models;

namespace WeatherDPE.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {
        }

        public DbSet<WeatherData> Weathers { get; set; }
    }
}
