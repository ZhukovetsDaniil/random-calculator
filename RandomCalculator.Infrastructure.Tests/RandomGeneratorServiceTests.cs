using System;
using System.Linq;
using NUnit.Framework;
using RandomCalculator.Infrastructure.Services;

namespace RandomCalculator.Infrastructure.Tests
{
    public class RandomGeneratorServiceTests
    {
        [TestCase(-1, 2, 10)]
        [TestCase(0, 2, 10)]
        [TestCase(1, -2, 10)]
        [TestCase(1, 2, -10)]
        public void GenerateRandomBySpecifiedSum_InvalidParameters_InvalidParameterException(int quantity, double maxValue, double totalSum)
        {
            var service = new RandomGeneratorService();

            Assert.Throws<ArgumentException>(() => service.GenerateRandomBySpecifiedSum(quantity, maxValue, totalSum));

        }

        [TestCase(45, 54, 2000)]
        [TestCase(45, 54, 1600)]
        [TestCase(45, 54, 1800)]
        public void GenerateRandomBySpecifiedSum_TestSum(int quantity, double maxValue, double totalSum)
        {
            var service = new RandomGeneratorService();

            var result = service.GenerateRandomBySpecifiedSum(quantity, maxValue, totalSum);
            Assert.AreEqual(totalSum, result.Sum());
            
        }

        [TestCase(45, 54, 2000)]
        [TestCase(45, 54, 1600)]
        [TestCase(45, 54, 1800)]
        public void GenerateRandomBySpecifiedSum_TestMin(int quantity, double maxValue, double totalSum)
        {
            var service = new RandomGeneratorService();

            var result = service.GenerateRandomBySpecifiedSum(quantity, maxValue, totalSum);
            Assert.Less(0, result.Min());
        }

        [TestCase(45, 54, 2000)]
        [TestCase(45, 54, 1600)]
        [TestCase(45, 54, 1800)]
        public void GenerateRandomBySpecifiedSum_TestMax(int quantity, double maxValue, double totalSum)
        {
            var service = new RandomGeneratorService();

            var result = service.GenerateRandomBySpecifiedSum(quantity, maxValue, totalSum);
            Assert.GreaterOrEqual((decimal)maxValue, result.Max());
        }

    }
}
