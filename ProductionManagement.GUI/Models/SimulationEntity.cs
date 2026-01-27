using System;

namespace ProductionManagement.GUI.Models
{
    public abstract class SimulationEntity
    {
        private static readonly Random _idRng = new();
        public int Id { get; set; }
        public string Name { get; set; }

        protected SimulationEntity()
        {
            Name = string.Empty;
            Id = _idRng.Next(1000, 9999);
        }

        protected SimulationEntity(string name)
        {
            Name = name;
            Id = _idRng.Next(1000, 9999);
        }

        public abstract string Description { get; }
        public abstract decimal GetDailyCost();
        public abstract string GetDescription();
    }
}