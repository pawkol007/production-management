namespace production_management.Models
{
    public class ProductionMachine(string name, decimal capacity, decimal cost) : SimulationEntity(name)
    {
        public decimal ProductionCapacity { get; set; } = capacity;
        public decimal MaintenanceCost { get; set; } = cost;

        public override decimal GetDailyCost()
        {
            return MaintenanceCost;
        }

        public override string GetDescription()
        {
            return $"Machine Capacity: {ProductionCapacity}, Cost: {MaintenanceCost:C}";
        }
    }
}