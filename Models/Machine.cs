namespace production_management.Models
{
    public abstract class Machine(string name)
    {
        public string Name { get; } = name;
    }
}
