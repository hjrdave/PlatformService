using AutoMapper;
using PlatformService.Models;
using PlatformService.Dtos;
namespace PlatformService.Profiles
{
    public class PlatformProfiles : Profile
    {
        public PlatformProfiles()
        {
            //source->target

            CreateMap<platform, PlateformReadDtos>();
            CreateMap<PlatformCreateDtos, platform>();
        }
    }

}