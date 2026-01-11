namespace production_management.Models
{
    public class Employee : SimulationEntity
    {
        public string LastName { get; set; } = "Unknown";
        public decimal DailyRate { get; set; }

        public Employee() : base("Unknown") { } 

        public Employee(string firstName, string lastName, decimal rate) : base(firstName)
        {
            LastName = lastName;
            DailyRate = rate;
        }

        public override decimal GetDailyCost()
        {
            return DailyRate;
        }

        public override string GetDescription()
        {
            return $"Employee: {LastName}, Rate: {DailyRate:C}";
        }

        public override string ToString()
        {
            return $"{Name} {LastName}";
        }
    }
}