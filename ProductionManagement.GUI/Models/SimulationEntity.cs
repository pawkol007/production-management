namespace production_management.Models
{
    public abstract class SimulationEntity(string name) : ICostGenerator
    {
        public string Name { get; set; } = name;
        public Guid Id { get; private set; } = Guid.NewGuid();

        public abstract string GetDescription();

        public virtual decimal GetDailyCost()
        {
            return 0;
        }

        public string GetName() => Name;

        public override string ToString()
        {
            return $"{Name} ({GetDescription()})";
        }
    }
}