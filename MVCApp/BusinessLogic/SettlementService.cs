using BusinessLogic.Base;
using Contracts.Mapper;
using Contracts.Repositories;
using Contracts.Services;
using Entities;
using Entities.DTOs;

namespace BusinessLogic
{
    public class SettlementService : BaseService<Settlement, SettlementDto>, ISettlementService
    {
        public SettlementService(ISettlementRepository repository, IMapperService mapperService) : base(repository, mapperService)
        {
        }
    }
}
