using BusinessLogic.Base;
using Contracts.Mapper;
using Contracts.Repositories;
using Contracts.Services;
using Entities;
using Entities.Exceptions;
using Entities.Models.DTOs;
using Entities.Pagination;

namespace BusinessLogic
{
    public class RouteService : BaseService<Route>, IRouteService
    {
        private readonly ISettlementService _settlementService;
        public RouteService(IRouteRepository repository, ISettlementService settlementService, IMapperService mapperService) : base(repository, mapperService)
        {
            _settlementService = settlementService;
        }

        public override PagedList<TDto> GetByPage<TDto>(PaginationQueryParameters parameters)
        {
            var routes = _repository
                .GetAllWithDependencies()
                .Skip((parameters.page - 1) * parameters.pageSize)
                .Take(parameters.pageSize);

            var count = _repository.Count();

            var routeDtos = _mapperService.Map<IEnumerable<Route>, IEnumerable<TDto>>(routes);

            return new PagedList<TDto>(routeDtos.ToList(), count, parameters.page, parameters.pageSize);
        }
        public override async Task<TDtoResult> CreateAsync<TDtoNewEntity, TDtoResult>(TDtoNewEntity dto)
        {
            var typedDto = dto as RouteCreateDto;

            if (typedDto == null)
                throw new BadRequestException("Dto is wrong format.");

            var startSettlement = await _settlementService.GetByIdAsync<Settlement>(typedDto.StartSettlementId);
            var endSettlement = await _settlementService.GetByIdAsync<Settlement>(typedDto.EndSettlementId);

            var newRoute = _mapperService.Map<RouteCreateDto, Route>(typedDto);

            newRoute.StartSettlement = startSettlement;
            newRoute.EndSettlement = endSettlement;

            return await base.CreateAsync<Route, TDtoResult>(newRoute);
        }
    }
}
