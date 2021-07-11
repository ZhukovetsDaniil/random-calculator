using AutoMapper;
using RandomCalculator.Domain.Models;
using RandomCalculator.Pages;

namespace RandomCalculator
{
    public class ApplicationProfile : Profile
    {
        public ApplicationProfile()
        {
            CreateMap<IndexModel, RandomCalculateInfo>();
        }
    }
}
