using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using production_management.Models;

namespace ProductionManagement.Services
{
    public static class EmployeeGenerator
    {
        public static List<Employee> GenerateFromJson(
            int count,
            string firstNamesPath,
            string lastNamesPath,
            decimal minSalary,
            decimal maxSalary)
        {
            if (!File.Exists(firstNamesPath) || !File.Exists(lastNamesPath))
                throw new FileNotFoundException("JSON files with names not found.");

            var firstNames = JsonSerializer.Deserialize<List<string>>(
                File.ReadAllText(firstNamesPath)) ?? new();

            var lastNames = JsonSerializer.Deserialize<List<string>>(
                File.ReadAllText(lastNamesPath)) ?? new();

            Random rand = new();
            List<Employee> employees = new();

            for (int i = 0; i < count; i++)
            {
                string firstName = firstNames[rand.Next(firstNames.Count)];
                string lastName = lastNames[rand.Next(lastNames.Count)];
                decimal salary = rand.Next((int)minSalary, (int)maxSalary);

                employees.Add(new Employee(firstName, lastName, salary));
            }

            return employees;
        }
    }
}
