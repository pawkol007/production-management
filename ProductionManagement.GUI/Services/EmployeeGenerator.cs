using ProductionManagement.GUI.Models;
using System.IO;
using System.Text.Json;

namespace ProductionManagement.GUI.Services
{
    public static class EmployeeGenerator
    {
        public static List<Employee> GenerateFromJson(int count, string namesPath, string lastNamesPath, decimal minRate, decimal maxRate)
        {
            var employees = new List<Employee>();
            if (!File.Exists(namesPath) || !File.Exists(lastNamesPath)) return employees;

            var names = JsonSerializer.Deserialize<List<string>>(File.ReadAllText(namesPath));
            var lastNames = JsonSerializer.Deserialize<List<string>>(File.ReadAllText(lastNamesPath));
            Random rng = new();

            for (int i = 0; i < count; i++)
            {
                string name = names[rng.Next(names.Count)];
                string lastName = lastNames[rng.Next(lastNames.Count)];
                decimal rate = rng.Next((int)minRate, (int)maxRate);

                employees.Add(new Employee(name, lastName, rate));
            }

            return employees;
        }
    }
}