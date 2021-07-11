using AutoMapper;
using RandomCalculator.Domain.Models;
using RandomCalculator.Pages;

namespace RandomCalculator.Profiles
{
    public class ApplicationProfile : Profile
    {
        public ApplicationProfile()
        {
            CreateMap<IndexModel, RandomCalculateInfo>();
        }
    }
}
