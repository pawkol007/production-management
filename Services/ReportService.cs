using production_management.Models;

namespace ProductionManagement.Services
{
    public static class ReportService
    {
        public static void PrintFinalReport(Company company)
        {
            Console.WriteLine("\n=== FINAL REPORT ===");
            Console.WriteLine($"Company: {company.Name}");
            Console.WriteLine($"Final budget: {company.Budget}");
            Console.WriteLine($"Employees: {company.Employees.Count}");
            Console.WriteLine($"Machines: {company.Machines.Count}");
        }
    }
}
