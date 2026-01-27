using System;

namespace ProductionManagement.GUI.Models
{
    public class Employee : SimulationEntity
    {
        public string LastName { get; set; }
        public decimal DailyRate { get; set; }

        public Employee() : base(string.Empty)
        {
            LastName = string.Empty;
            DailyRate = 0m;
        }

        public Employee(string firstName, string lastName, decimal rate) : base(firstName)
        {
            LastName = lastName;
            DailyRate = rate;
        }

        private static readonly Random _rng = new();

        public void WorkDay()
        {
            if (_rng.NextDouble() > 0.95)
            {
                DailyRate += 10;
            }
        }

        public override decimal GetDailyCost() => DailyRate;

        public override string Description => $"[ID:{Id}] {Name} {LastName} - {DailyRate:C}";

        public override string GetDescription() => Description;

        public override string ToString() => $"{Id}: {Name} {LastName}";
    }
}