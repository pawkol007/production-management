using production_management.Models;
using production_management.Services;

namespace production_management.src
{
    internal class Program
    {
        static void Main()
        {
            Console.Clear();
            Console.WriteLine("=== PRODUCTION MANAGEMENT SIMULATION ===");

            Console.Write("Company name: ");
            string name = Console.ReadLine()!;

            Console.Write("Starting budget: ");
            decimal budget = decimal.Parse(Console.ReadLine()!);

            Console.Write("Simulation days: ");
            int days = int.Parse(Console.ReadLine()!);

            Company company = new(name, budget);

            Console.Write("Number of employees: ");
            int empCount = int.Parse(Console.ReadLine()!);

            company.Employees.AddRange(
                EmployeeGenerator.GenerateFromJson(
                    empCount,
                    "Data/first_names.json",
                    "Data/last_names.json",
                    200, 400));

            company.Machines.Add(
                new ProductionMachine("Main Line", 1200, 300));

            company.Machines.Add(
                new ProductionMachine("Secondary Line", 800, 200));

            Console.WriteLine("\n--- STARTING SIMULATION ---\n");
            SimulationEngine.Run(company, days);

            ReportService.PrintFinalReport(company);

            Console.ReadKey();
        }
    }
}
