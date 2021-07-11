using System;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using RandomCalculator.Application.Interfaces;
using RandomCalculator.Domain.Models;

namespace RandomCalculator.Pages
{
    public class IndexModel : PageModel
    {
        private readonly IRandomFileGenerator _randomFileGenerator;
        private readonly IMapper _mapper;

        [BindProperty]
        [Required(ErrorMessage = "Не задана максимальная масса")]
        [Range(0, double.MaxValue, ErrorMessage = "Масса должна быть положительной")]
        public double MaxWeight { get; set; }

        [BindProperty]
        [Required(ErrorMessage = "Не задана цена")]
        [Range(0, double.MaxValue, ErrorMessage = "Цена должна быть положительной")]
        public double Price { get; set; }

        [BindProperty]
        [Required(ErrorMessage = "Не задано количество строк")]
        [Range(1, int.MaxValue, ErrorMessage = "Количество строк должно быть больше 0")]
        public int RowQuantity { get; set; }

        [BindProperty]
        [Required(ErrorMessage = "Не задана общая масса")]
        [Range(0, double.MaxValue, ErrorMessage = "Масса должна быть положительной")]
        public int TotalWeight { get; set; }

        public IndexModel(IRandomFileGenerator randomFileGenerator, IMapper mapper)
        {
            _randomFileGenerator = randomFileGenerator;
            _mapper = mapper;
        }

        public async Task<IActionResult> OnPost()
        {
            var info = _mapper.Map<RandomCalculateInfo>(this);

            var stream = await _randomFileGenerator.GenerateRandomSumFile(info);

            return new FileStreamResult(stream, "text/plain")
            {
                FileDownloadName = $"numbers({info.TotalWeight}).txt"
            };
        }
    }
}