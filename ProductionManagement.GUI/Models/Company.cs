using System.Collections.Generic;
using System.Linq;

namespace ProductionManagement.GUI.Models
{
    public class Company
    {
        public string Name { get; set; }
        public decimal Budget { get; private set; }
        public int RawMaterials { get; set; }
        public int ProductStock { get; set; }
        public int DailyProduction { get; set; }

        public List<Employee> Employees { get; set; } = new List<Employee>();
        public List<ProductionMachine> Machines { get; set; } = new List<ProductionMachine>();

        public Company()
        {
            Name = string.Empty;
            Budget = 0m;
            RawMaterials = 0;
            ProductStock = 0;
        }

        public Company(string name, decimal initialBudget, int initialMaterials)
        {
            Name = name;
            Budget = initialBudget;
            RawMaterials = initialMaterials;
        }

        public void ApplyDay(decimal income, decimal costs)
        {
            Budget += income;
            Budget -= costs;
        }

        public bool ConsumeMaterials(int amount)
        {
            if (RawMaterials >= amount)
            {
                RawMaterials -= amount;
                return true;
            }
            return false;
        }

        public void AddProductsToStock(int amount)
        {
            ProductStock += amount;
        }

        public int SellProducts(int amountToSell)
        {
            if (ProductStock >= amountToSell)
            {
                ProductStock -= amountToSell;
                return amountToSell;
            }
            else
            {
                int sold = ProductStock;
                ProductStock = 0;
                return sold;
            }
        }

        public decimal DailyEmployeeCost() => Employees.Sum(e => e.GetDailyCost());
        public decimal DailyMachineCost() => Machines.Sum(m => m.GetDailyCost());
        public decimal DailyProductionCapacity() => Machines.Sum(m => m.ProductionCapacity);
    }
}