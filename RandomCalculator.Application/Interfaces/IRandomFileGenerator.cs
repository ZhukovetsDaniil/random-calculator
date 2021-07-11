using System.IO;
using System.Threading.Tasks;
using RandomCalculator.Domain.Models;

namespace RandomCalculator.Application.Interfaces
{
    public interface IRandomFileGenerator
    {
        Task<Stream> GenerateRandomSumFile(RandomCalculateInfo info);
    }
}
