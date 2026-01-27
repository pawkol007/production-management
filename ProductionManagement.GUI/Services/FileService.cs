using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using ProductionManagement.GUI.Models;

namespace ProductionManagement.GUI.Services
{
    public static class FileService
    {
        private static readonly JsonSerializerOptions _jsonOptions = new JsonSerializerOptions { WriteIndented = true };

        public static List<Employee> LoadEmployees(string path)
        {
            if (!File.Exists(path)) return new List<Employee>();
            var json = File.ReadAllText(path);
            return JsonSerializer.Deserialize<List<Employee>>(json, _jsonOptions) ?? new List<Employee>();
        }

        public static void SaveEmployees(List<Employee> employees, string path)
        {
            try
            {
                EnsureDirectory(path);
                var json = JsonSerializer.Serialize(employees, _jsonOptions);
                File.WriteAllText(path, json);
            }
            catch (Exception ex)
            {
                throw new IOException($"SaveEmployees failed for path '{path}': {ex.Message}", ex);
            }
        }

        public static Company? LoadCompany(string path)
        {
            if (!File.Exists(path)) return null;
            var json = File.ReadAllText(path);
            return JsonSerializer.Deserialize<Company>(json, _jsonOptions);
        }

        public static void SaveCompany(Company company, string path)
        {
            try
            {
                EnsureDirectory(path);
                var json = JsonSerializer.Serialize(company, _jsonOptions);
                File.WriteAllText(path, json);
            }
            catch (Exception ex)
            {
                throw new IOException($"SaveCompany failed for path '{path}': {ex.Message}", ex);
            }
        }

        private static void EnsureDirectory(string path)
        {
            var dir = Path.GetDirectoryName(path);
            if (string.IsNullOrEmpty(dir)) return;
            if (!Directory.Exists(dir))
                Directory.CreateDirectory(dir);
        }
    }
}