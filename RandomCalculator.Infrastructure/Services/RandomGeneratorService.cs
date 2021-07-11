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

            var middle = Math.Round((decimal)totalSum / numberQuantity, 1);

            var limit = maxVal - middle;

            var minVal = maxVal - (int)(maxVal / 4);

            for(int i = 0; i < numberQuantity - 1;)
            {
                list.Add(Rand(minVal, maxVal));

                var error = middle - list[i];

                if (error < limit)
                {
                    list.Add(middle + error);
                    i++;
                }
                else
                {
                    var tmp = limit * (numberQuantity - list.Count);

                    if (error > tmp)
                    {
                        return null;
                    }

                    var itemError = Math.Round(error / numberQuantity, 1);

                    itemError = Math.Max(0.1m, itemError);

                    i++;

                    for(; i < numberQuantity && error > 0; i++)
                    {
                        if (error < limit)
                        {
                            list.Add(middle + error);
                            error = 0;
                        }
                        else
                        {
                            var curError = Rand(itemError, limit);
                            list.Add(middle + curError);
                            error -= curError;
                        }
                    }
                }
            }

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
