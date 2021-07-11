using System.Collections.Generic;

namespace RandomCalculator.Application.Interfaces
{
    public interface IRandomGenerator
    {
        IEnumerable<decimal> GenerateRandomBySpecifiedSum(int numberQuantity, double maxValue, double totalSum);
    }
}
