using production_management.Models;

namespace ProductionManagement.GUI.Models
{
    public class Company(string name, decimal initialBudget, int initialMaterials)
    {
        public string Name { get; set; } = name;
        public decimal Budget { get; private set; } = initialBudget;
        public int RawMaterials { get; set; } = initialMaterials;
        public int ProductStock { get; set; } = 0;
        public int DailyProduction { get; set; }

        public List<Employee> Employees { get; set; } = [];
        public List<ProductionMachine> Machines { get; set; } = [];

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