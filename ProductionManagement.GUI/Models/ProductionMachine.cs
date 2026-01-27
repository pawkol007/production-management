using System;

namespace ProductionManagement.GUI.Models
{
    public class ProductionMachine : SimulationEntity
    {
        public decimal ProductionCapacity { get; set; }
        public decimal MaintenanceCost { get; set; }

        public ProductionMachine(string name, decimal capacity, decimal cost) : base(name)
        {
            ProductionCapacity = capacity;
            MaintenanceCost = cost;
        }

        public override decimal GetDailyCost()
        {
            return (ProductionCapacity + MaintenanceCost) / 30m;
        }

        public override string GetDescription() => Description;

        public override string Description => $"Machine \"{Name}\" Capacity: {ProductionCapacity}, Cost: {MaintenanceCost:C}";
    }
}