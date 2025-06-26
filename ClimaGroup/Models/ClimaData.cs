using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClimaGroup.Models
{
    public class ClimaData
    {
        public DateTime Hora { get; set; }
        public double Temperatura { get; set; }
        public int Humedad { get; set; }
        public double Lluvia { get; set; }
    }
}
