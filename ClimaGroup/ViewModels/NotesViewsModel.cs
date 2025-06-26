using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace ClimaGroup.ViewModels
{
    public class NotesViewModel : INotifyPropertyChanged
    {
        public ObservableCollection<Note> Notes { get; set; } = new();

        private string _titulo;
        public string Titulo
        {
            get => _titulo;
            set { _titulo = value; OnPropertyChanged(nameof(Titulo)); }
        }

        private string _contenido;
        public string Contenido
        {
            get => _contenido;
            set { _contenido = value; OnPropertyChanged(nameof(Contenido)); }
        }

        public ICommand AgregarNotaCommand { get; }
        public ICommand EliminarNotaCommand { get; }

        public NotesViewModel()
        {
            AgregarNotaCommand = new Command(AgregarNota);
            EliminarNotaCommand = new Command<Note>(EliminarNota);
        }

        private void AgregarNota()
        {
            if (!string.IsNullOrWhiteSpace(Titulo))
            {
                Notes.Add(new Note { Titulo = this.Titulo, Contenido = this.Contenido });
                Titulo = string.Empty;
                Contenido = string.Empty;
            }
        }

        private void EliminarNota(Note note)
        {
            if (Notes.Contains(note))
                Notes.Remove(note);
        }

        public event PropertyChangedEventHandler PropertyChanged;
        void OnPropertyChanged(string name) =>
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
    }
}
