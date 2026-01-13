using ProductionManagement.GUI.Models;
using System.IO;
using System.Text.Json;

namespace ProductionManagement.GUI.Services
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
            var options = new JsonSerializerOptions { WriteIndented = true };
            string json = JsonSerializer.Serialize(company, options);
            File.WriteAllText(path, json);
        }
    }
}