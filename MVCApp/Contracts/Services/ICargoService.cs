using Contracts.Services.Base;
using Entities;
using Entities.DTOs;

namespace Contracts.Services
{
    public interface ICargoService : IBaseEntityService<Cargo, CargoDto>
    {
    }
}
