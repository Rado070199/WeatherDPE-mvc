using System.ComponentModel.DataAnnotations;

namespace WeatherDPE.Models
{
    public class WeatherData
    {
        [Key]
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public double Temperature { get; set; }
        public double Pressure { get; set; }
        public double WindSpeed { get; set; }
        public string? Icon { get; set; }
    }
}
