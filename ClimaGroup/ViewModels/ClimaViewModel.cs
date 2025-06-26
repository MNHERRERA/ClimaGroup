using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using static Microsoft.Maui.ApplicationModel.Permissions;

namespace ClimaGroup.ViewModels
{
    public class ClimaViewModel : INotifyPropertyChanged
    {
        await Permissions.RequestAsync<Permissions.LocationWhenInUse>();

        private readonly ClimaService _climaService = new();

        private ClimaData _clima;
        public ClimaData Clima
        {
            get => _clima;
            set { _clima = value; OnPropertyChanged(nameof(Clima)); }
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
                var location = await Geolocation.GetLastKnownLocationAsync()
                               ?? await Geolocation.GetLocationAsync();

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
        void OnPropertyChanged(string name) =>
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
    }
}
