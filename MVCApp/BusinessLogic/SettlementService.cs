using BusinessLogic.Base;
using Contracts.Mapper;
using Contracts.Repositories;
using Contracts.Services;
using Entities;

namespace BusinessLogic
{
    public class SettlementService : BaseService<Settlement>, ISettlementService
    {
        public SettlementService(ISettlementRepository repository, IMapperService mapperService) : base(repository, mapperService)
        {
        }
    }
}
