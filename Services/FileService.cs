using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using production_management.Models;

namespace production_management.Services
{
    public static class FileService
    {
        public static List<Employee> LoadEmployees(string path)
        {
            if (!File.Exists(path)) return [];

            string json = File.ReadAllText(path);
            return JsonSerializer.Deserialize<List<Employee>>(json) ?? [];
        }

        public static void SaveCompany(Company company, string path)
        {
            JsonSerializerOptions jsonSerializerOptions = new() { WriteIndented = true };
            var options = jsonSerializerOptions;
            string json = JsonSerializer.Serialize(company, options);
            File.WriteAllText(path, json);
        }
    }
}