using AutoMapper;
using Entities;
using Entities.DTOs;

namespace MapperHelper.Profiles
{
    public class SettlementProfile : Profile
    {
        public SettlementProfile()
        {
            CreateMap<Settlement, SettlementDto>();
        }
    }
}
