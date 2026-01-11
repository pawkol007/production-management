using production_management.Models;

namespace ProductionManagement.Services
{
    public static class SimulationEngine
    {
        public static void Run(Company company, int days)
        {
            for (int day = 1; day <= days; day++)
            {
                decimal income = company.DailyProduction();
                decimal costs = company.DailyEmployeeCost() + company.DailyMachineCost();

                company.ApplyDay(income, costs);

                Console.WriteLine(
                    $"Day {day}: +{income} / -{costs} | Budget: {company.Budget}");
            }
        }
    }
}
