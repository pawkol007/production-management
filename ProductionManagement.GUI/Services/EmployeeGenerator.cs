using ProductionManagement.GUI.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;

namespace ProductionManagement.GUI.Services
{
    public static class EmployeeGenerator
    {
        private static readonly Random _rng = new();

        public static List<Employee> GenerateFromJson(int count, string namesPath, string lastNamesPath, decimal minRate, decimal maxRate)
        {
            var employees = new List<Employee>();
            if (!File.Exists(namesPath) || !File.Exists(lastNamesPath)) return employees;

            var names = JsonSerializer.Deserialize<List<string>>(File.ReadAllText(namesPath));
            var lastNames = JsonSerializer.Deserialize<List<string>>(File.ReadAllText(lastNamesPath));
            if (names == null || names.Count == 0 || lastNames == null || lastNames.Count == 0) return employees;

            for (int i = 0; i < count; i++)
            {
                string name = names[_rng.Next(names.Count)].Trim();
                string lastName = lastNames[_rng.Next(lastNames.Count)].Trim();
                decimal rate = _rng.Next((int)minRate, (int)maxRate);
                employees.Add(new Employee(name, lastName, rate));
            }

            return employees;
        }
    }
}