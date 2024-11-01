using AutoMapper;
using Entities;
using Entities.DTOs;

namespace MapperHelper.Profiles
{
    public class CargoProfile : Profile
    {
        public CargoProfile()
        {
            CreateMap<Cargo, CargoDto>();
        }
    }
}
