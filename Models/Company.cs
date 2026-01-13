using System.Collections.Generic;
using System.Linq;

namespace production_management.Models
{
    public class Company(string name, decimal initialBudget)
    {
        public string Name { get; set; } = name;
        public decimal Budget { get; private set; } = initialBudget;

        public List<Employee> Employees { get; set; } = [];
        public List<ProductionMachine> Machines { get; set; } = [];

        public void ApplyDay(decimal income, decimal costs)
        {
            Budget += income;
            Budget -= costs;
        }

        public decimal DailyEmployeeCost()
        {
            return Employees.Sum(e => e.GetDailyCost());
        }

        public decimal DailyMachineCost()
        {
            return Machines.Sum(m => m.GetDailyCost());
        }

        public decimal DailyProduction()
        {
            return Machines.Sum(m => m.ProductionCapacity);
        }
    }
}