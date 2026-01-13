namespace ProductionManagement.Models
{
    public interface IProducing
    {
        decimal ProducePerDay();
        decimal DailyCost();
    }
}
