using AutoMapper;
using Healty.Controllers.Api;
using Healty.Core.Dtos;
using Healty.Core.Models;

namespace Healty.App_Start
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            Mapper.CreateMap<ApplicationUser, UserDto>();
        }
    }
}