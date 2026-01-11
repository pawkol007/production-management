namespace ProductionManagement.Models
{
    public abstract class Machine
    {
        public string Name { get; }

        protected Machine(string name)
        {
            Name = name;
        }
    }
}
