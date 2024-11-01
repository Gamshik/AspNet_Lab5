using Contracts.Services.Base;
using Entities;

using Entities.DTOs;
namespace Contracts.Services
{
    public interface ISettlementService : IBaseEntityService<Settlement, SettlementDto>
    {
    }
}
