using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClimaGroup.Models
{
    public class MiembroEquipo
    {
        public string Nombre { get; set; }
        public int Edad { get; set; }
        public string DeporteFavorito { get; set; }
        public string Imagen { get; set; } // Ruta del recurso de imagen
    }
}
