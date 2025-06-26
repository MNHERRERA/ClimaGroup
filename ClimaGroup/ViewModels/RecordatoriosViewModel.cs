using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ClimaGroup.Models;
using ClimaGroup.Services;
using System.Windows.Input;

namespace ClimaGroup.ViewModels
{
    public class RecordatoriosViewModel : INotifyPropertyChanged
    {
        public ObservableCollection<Recordatorio> ListaRecordatorios { get; } = new();

        private string _texto;
        public string Texto
        {
            get => _texto;
            set { _texto = value; OnPropertyChanged(nameof(Texto)); }
        }

        private TimeSpan _fechaHora;
        public TimeSpan FechaHora
        {
            get => _fechaHora;
            set { _fechaHora = value; OnPropertyChanged(nameof(FechaHora)); }
        }

        public ICommand AgregarCommand { get; }
        public ICommand EliminarCommand { get; }

        public RecordatoriosViewModel()
        {
            AgregarCommand = new Command(Agregar);
            EliminarCommand = new Command<Recordatorio>(Eliminar);

            _ = CargarRecordatoriosAsync();
        }

        private async Task CargarRecordatoriosAsync()
        {
            var datos = await RecordatorioStorageService.CargarAsync();
            foreach (var r in datos)
                ListaRecordatorios.Add(r);
        }

        private async void Agregar()
        {
            var nuevo = new Recordatorio { Texto = Texto, FechaHora = FechaHora, Activo = true };
            ListaRecordatorios.Add(nuevo);
            await Guardar();
            Texto = string.Empty;
        }

        private async void Eliminar(Recordatorio r)
        {
            if (ListaRecordatorios.Contains(r))
                ListaRecordatorios.Remove(r);
            await Guardar();
        }

        private async Task Guardar() =>
            await RecordatorioStorageService.GuardarAsync(ListaRecordatorios.ToList());

        public event PropertyChangedEventHandler PropertyChanged;
        void OnPropertyChanged(string name) =>
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
    }
}
