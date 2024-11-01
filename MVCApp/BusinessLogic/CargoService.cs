using BusinessLogic.Base;
using Contracts.Mapper;
using Contracts.Repositories;
using Contracts.Services;
using Entities;
using Entities.DTOs;

namespace BusinessLogic
{
    public class CargoService : BaseService<Cargo, CargoDto>, ICargoService
    {
        public CargoService(ICargoRepository repository, IMapperService mapperService) : base(repository, mapperService)
        {
        }
    }
}
