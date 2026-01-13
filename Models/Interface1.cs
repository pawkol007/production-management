using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace production_management.Models
{
    public interface ICostGenerator
    {
        decimal GetDailyCost();
        string GetName();
    }
}