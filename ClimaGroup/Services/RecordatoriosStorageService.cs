using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using ClimaGroup.Models;

namespace ClimaGroup.Services
{
    class RecordatoriosStorageService
    {
        private static readonly string filePath = Path.Combine(FileSystem.AppDataDirectory, "recordatorios.json");

        public static async Task<List<Recordatorio>> CargarAsync()
        {
            if (!File.Exists(filePath))
                return new List<Recordatorio>();

            var json = await File.ReadAllTextAsync(filePath);
            return JsonSerializer.Deserialize<List<Recordatorio>>(json) ?? new();
        }

        public static async Task GuardarAsync(List<Recordatorio> recordatorios)
        {
            var json = JsonSerializer.Serialize(recordatorios);
            await File.WriteAllTextAsync(filePath, json);
        }
    }
}
