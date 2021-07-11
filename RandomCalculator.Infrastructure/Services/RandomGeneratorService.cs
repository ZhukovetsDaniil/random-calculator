using System;
using System.Collections.Generic;
using System.Linq;
using RandomCalculator.Application.Interfaces;

namespace RandomCalculator.Infrastructure.Services
{
    public class RandomGeneratorService : IRandomGenerator
    {
        private readonly Random random = new Random();


        public IEnumerable<decimal> GenerateRandomBySpecifiedSum(int numberQuantity, double maxValue, double totalSum)
        {
            if (numberQuantity <= 0)
            {
                throw new ArgumentException("Shold be great than zero", nameof(numberQuantity));
            }

            if (maxValue <= 0)
            {
                throw new ArgumentException("Shold be great than zero", nameof(maxValue));
            }

            if (totalSum <= 0)
            {
                throw new ArgumentException("Shold be great than zero", nameof(totalSum));
            }

            if (totalSum / numberQuantity > maxValue)
            {
                throw new ArgumentException("Max value cannot be less than averege", nameof(maxValue));
            }

            var maxVal = (decimal)maxValue;

            var list = new List<decimal>(numberQuantity);

            var middle = (int)(totalSum / numberQuantity);

            var error = (decimal)totalSum - middle * numberQuantity;

            for (int i = 0; i < numberQuantity / 2; i++)
            {
                var tmp = middle * 2;
                var rnd = Rand(tmp - maxVal, maxVal);

                list.Add(rnd);
                list.Add(tmp - rnd);
            }

            if (list.Count + 1 == numberQuantity)
            {
                list.Add(middle);
            }

            for (int j = 0; j < 3; j++)
            {
                var average = list.Average();

                var itemError = (decimal)Math.Round(error / list.Count(i => i < average), 1);

                for (int i = 0; i < numberQuantity / 2; i++)
                {
                    if (list[i] < middle)
                    {
                        list[i] += itemError;
                    }
                }

                error = (decimal)totalSum - list.Sum();
            }

            list[^1] += error;

            return list;
        }

        private decimal Rand(decimal minValue, decimal maxValue)
        {
            var range = maxValue - minValue;

            var rnd = Math.Round((decimal)random.NextDouble() * range + minValue, 1);

            return rnd;
        }
    }
}
