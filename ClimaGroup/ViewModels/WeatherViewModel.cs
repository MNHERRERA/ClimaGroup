using ClimaGroup.Models;
using ClimaGroup.Repositories;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace ClimaGroup.ViewModels
{
    class WeatherViewModel
    {
        private WeatherData _weatherData;
        public WeatherData WeatherData
        {
            get => _weatherData;
            set
            {
                if (_weatherData != value)
                {
                    _weatherData = value;
                    OnPropertyChanged();
                }
            }
        }
        public WeatherViewModel()
        {
            _weatherData = new WeatherData(); // Initialize with an empty WeatherData object
            GetCurrentWeatherAsync();  

        }
        public async Task GetCurrentWeatherAsync()
        {
            WeatherRepository weatherRepository = new WeatherRepository();
            WeatherData = await weatherRepository.GetWeatherCurrentLocationAsync();
        }
        public event PropertyChangedEventHandler? PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string name = "") => 
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
    }
}
