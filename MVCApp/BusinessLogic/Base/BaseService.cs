﻿using Contracts.Mapper;
using Contracts.Repositories.Base;
using Contracts.Services.Base;
using Entities.Base;
using Entities.Pagination;

namespace BusinessLogic.Base
{
    public class BaseService<TDb, TDto> : IBaseEntityService<TDb, TDto>
        where TDb : EntityBase
        where TDto : EntityBaseDto
    {
        protected readonly IRepositoryBase<TDb> _repository;
        protected readonly IMapperService _mapperService;
        public BaseService(IRepositoryBase<TDb> repository, IMapperService mapperService)
        {
            _repository = repository;
            _mapperService = mapperService;
        }
        public virtual PagedList<TDto> GetByPage(PaginationQueryParameters parameters)
        {
            var entities = _repository
                .GetAll()
                .Skip((parameters.page - 1) * parameters.pageSize)
                .Take(parameters.pageSize);

            var count = _repository.Count();

            var entitiesDtos = _mapperService.Map<IEnumerable<TDb>, IEnumerable<TDto>>(entities);

            return new PagedList<TDto>(entitiesDtos.ToList(), count, parameters.page, parameters.pageSize);
        }
        public async Task<TDto> GetByIdAsync(Guid id)
        {
            var dbEntity = await _repository.FindByIdAsync(id);

            return _mapperService.Map<TDb, TDto>(dbEntity);
        }

        public async Task<TDto> CreateAsync(TDto dto)
        {
            var mapEntity = _mapperService.Map<TDto, TDb>(dto);

            var dbEntity = await _repository.CreateAsync(mapEntity);

            return _mapperService.Map<TDb, TDto>(dbEntity);
        }
        public async Task DeleteByIdAsync(Guid id) => await _repository.DeleteByIdAsync(id);
        public async Task<TDto> UpdateAsync(TDto dto)
        {
            var mapEntity = _mapperService.Map<TDto, TDb>(dto);

            var dbEntity = await _repository.UpdateAsync(mapEntity);

            return _mapperService.Map<TDb, TDto>(dbEntity);
        }
    }
}
