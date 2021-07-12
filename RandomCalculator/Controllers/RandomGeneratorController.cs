using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using RandomCalculator.Application.Interfaces;
using RandomCalculator.Domain.Models;

namespace RandomCalculator {
    public class RandomGeneratorController : Controller
    {
        private readonly IRandomFileGenerator generator;

        public RandomGeneratorController(IRandomFileGenerator generator)
        {
            this.generator = generator;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> GetRandomFile(RandomCalculateInfo info)
        {
            var stream = await generator.GenerateRandomSumFile(info);

            return new FileStreamResult(stream, "text/plain")
            {
                FileDownloadName = $"numbers({info.TotalWeight}).txt"
            };
        }
    }
}