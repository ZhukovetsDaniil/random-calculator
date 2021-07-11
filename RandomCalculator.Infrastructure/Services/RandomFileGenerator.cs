using System;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using RandomCalculator.Application.Interfaces;
using RandomCalculator.Domain.Models;

namespace RandomCalculator.Infrastructure.Services
{
    public class RandomFileGenerator : IRandomFileGenerator
    {
        private readonly IRandomGenerator _generator;

        public RandomFileGenerator(IRandomGenerator generator)
        {
            _generator = generator;
        }

        public async Task<Stream> GenerateRandomSumFile(RandomCalculateInfo info)
        {
            var stream = new MemoryStream();

            var list = _generator.GenerateRandomBySpecifiedSum(info.RowQuantity, info.MaxWeight, info.TotalWeight);

            foreach (var item in list)
            {
                await stream.WriteAsync(Encoding.ASCII.GetBytes($"{item} {item * (decimal)info.Price}\n"));
            }
            stream.Position = 0;

            return stream;
        }
    }
}
