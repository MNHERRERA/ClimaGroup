using System;
using System.ComponentModel;
using System.Threading.Tasks;
using System.Windows.Input;
using ClimaGroup.Models;
using ClimaGroup.Services;
using Microsoft.Maui.ApplicationModel;
using Microsoft.Maui.Devices.Sensors;

namespace ClimaGroup.ViewModels
{
    public class ClimaViewModel : INotifyPropertyChanged
    {
        private readonly ClimaService _climaService = new();

        private ClimaData _clima;
        public ClimaData Clima
        {
            get => _clima;
            set
            {
                _clima = value;
                OnPropertyChanged(nameof(Clima));
            }
        }

        public ICommand CargarClimaCommand { get; }

        public ClimaViewModel()
        {
            CargarClimaCommand = new Command(async () => await CargarClimaAsync());
            _ = CargarClimaAsync();
        }

        private async Task CargarClimaAsync()
        {
            try
            {
                // Pedir permiso de geolocalización
                var status = await Permissions.RequestAsync<Permissions.LocationWhenInUse>();
                if (status != PermissionStatus.Granted)
                {
                    Console.WriteLine("Permiso de ubicación no concedido");
                    return;
                }

                Location location = await Geolocation.GetLastKnownLocationAsync();

                if (location == null)
                {
                    location = await Geolocation.GetLocationAsync();
                }

                if (location != null)
                {
                    Clima = await _climaService.ObtenerClimaAsync(location.Latitude, location.Longitude)
                            ?? new ClimaData();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[Error Geolocalización] {ex.Message}");
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged(string name) =>
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
    }
}
