using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ClimaGroup.Models;

namespace ClimaGroup.ViewModels
{
    public class AboutViewModel
    {
        public ObservableCollection<MiembroEquipo> Miembros { get; set; }

        public AboutViewModel()
        {
            Miembros = new ObservableCollection<MiembroEquipo>
            {
                new MiembroEquipo { Nombre = "Mateo Ortega", Edad = 20, DeporteFavorito = "Cocinar", Imagen = "mateo.png" },
                new MiembroEquipo { Nombre = "Mateo Herrera", Edad = 23, DeporteFavorito = "Downhill", Imagen = "teo.png" },
            };
        }
    }
}
