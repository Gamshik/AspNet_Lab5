using BusinessLogic.Base;
using Contracts.Mapper;
using Contracts.Repositories;
using Contracts.Services;
using Entities;
using Entities.DTOs;
using Entities.Pagination;

namespace BusinessLogic
{
    public class RouteService : BaseService<Route, RouteDto>, IRouteService
    {
        public RouteService(IRouteRepository repository, IMapperService mapperService) : base(repository, mapperService)
        {
        }

        public override PagedList<RouteDto> GetByPage(PaginationQueryParameters parameters)
        {
            var routes = _repository
                .GetAllWithDependencies()
                .Skip((parameters.page - 1) * parameters.pageSize)
                .Take(parameters.pageSize);

            var count = _repository.Count();

            var routeDtos = _mapperService.Map<IEnumerable<Route>, IEnumerable<RouteDto>>(routes);

            return new PagedList<RouteDto>(routeDtos.ToList(), count, parameters.page, parameters.pageSize);
        }
    }
}
