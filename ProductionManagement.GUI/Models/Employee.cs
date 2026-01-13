using production_management.Models;

namespace ProductionManagement.GUI.Models
{
    public class Employee : SimulationEntity
    {
        public string LastName { get; set; } = string.Empty;
        public decimal DailyRate { get; set; }
        public int ExperienceLevel { get; private set; } = 1;

        public Employee() : base("Unknown") { }

        public Employee(string firstName, string lastName, decimal rate) : base(firstName)
        {
            LastName = lastName;
            DailyRate = rate;
        }

        public void WorkDay()
        {
            Random r = new();
            if (r.NextDouble() > 0.95) 
            {
                ExperienceLevel++;
                DailyRate += 10;
            }
        }

        public override decimal GetDailyCost() => DailyRate;

        public override string GetDescription() => $"Lvl {ExperienceLevel} {LastName}, Rate: {DailyRate:C}";

        public override string ToString()
        {
            string shortId = Id.ToString()[..4].ToUpper();
            return $"[{shortId}] {Name} {LastName} (Lvl {ExperienceLevel})";
        }
    }
}