namespace production_management.Models
{
    public interface ICostGenerator
    {
        decimal GetDailyCost();
        string GetName();
    }
}