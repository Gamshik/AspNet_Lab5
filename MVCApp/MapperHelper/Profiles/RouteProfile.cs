using AutoMapper;
using Entities;
using Entities.DTOs;

namespace MapperHelper.Profiles
{
    public class RouteProfile : Profile
    {
        public RouteProfile()
        {

            CreateMap<Route, RouteDto>();
        }
    }
}
